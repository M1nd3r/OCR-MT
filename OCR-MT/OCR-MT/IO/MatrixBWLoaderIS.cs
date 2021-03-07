using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OCR_MT.Core;
using OCR_MT.Imaging;
using OCR_MT.Logging;
using OCR_MT.Utils;
using OCR_MT.MT;
using static OCR_MT.Utils.Constants;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.IO {
    class MatrixBWLoaderIS:IMatrixBWLoader {
        public MatrixBW Load(string path) {
            MatrixBW m;
            using (SixLabors.ImageSharp.Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(path)) {
                m = new MatrixBW(image.Width, image.Height);
                for (int x = 0; x < m.Width; x++) {
                    for (int y = 0; y < m.Height; y++) {
                        m[x, y] = (image[x, y].R > 0) ? (byte)0 : (byte)1;
                        var a = image[x, y];
                    }
                }
            }
            return m;
        }
        public IEnumerable<MatrixBW> Load(ICollection<string> paths) {
            List<MatrixBW> r = new List<MatrixBW>();
            foreach (var path in paths)
                r.Add(Load(path));
            return r;
        }
    }

    
    

    class MatrixBWParserMT : IMatrixBWParser<BW>, IMultiThread,IDisposable {
        private ILogger logger = LoggerFactory.GetLogger();
        private bool[] _open;
        private static object _lock = new object();
        private int[] _x;
        private int[] _y;
        private MatrixBW[] _m;
        private Dictionary<int, int> _thDic;
        public int NumberOfThreads { get; }
        public MatrixBWParserMT(int numberOfThreads) {
            logger.Out(nameof(MatrixBWParserMT) +".Constructor"+ ":  Start nt="+numberOfThreads.ToString()+")");
            _x = new int[numberOfThreads];
            _y = new int[numberOfThreads];
            _thDic = new Dictionary<int, int>();
            _open =new bool[numberOfThreads];
            _open.SetAllToTrue();
            _m = new MatrixBW[numberOfThreads];            
            this.NumberOfThreads = numberOfThreads;
        }
        public void InitializeThread() {
            logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ":  Waiting for lock");
            lock (_lock) {
                logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Lock granted");
                if (!_thDic.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                    _thDic.Add(Thread.CurrentThread.ManagedThreadId, _index++);
                logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Added thread to dictionary, new n="+_thDic.Count);
                logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Lock to be released");
            }
            logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Lock released");
        }
        public MatrixBW Parse(IImage<BW> image) {
            logger.Out(nameof(MatrixBWParserMT) + nameof(Parse) + ": Start");
            _m[GetIndex] = new MatrixBW(image.Width, image.Height);
            logger.Out(nameof(MatrixBWParserMT) + nameof(Parse) + ": Entering for cycle");
            for (_x[GetIndex] = 0; _x[GetIndex] < image.Width; _x[GetIndex]++) {
                for (_y[GetIndex] = 0; _y[GetIndex] < image.Height; _y[GetIndex]++) {
                    _m[GetIndex][_x[GetIndex], _y[GetIndex]] = image[_x[GetIndex], _y[GetIndex]].Intensity;
                }
            }
            logger.Out(nameof(MatrixBWParserMT) + nameof(Parse) + ": Exited for cycle, returning value soon");
            return _m[GetIndex];
        }
        private int GetIndex => _thDic.GetValueOrDefault(Thread.CurrentThread.ManagedThreadId);
        public void Dispose() {
            logger.Out(nameof(MatrixBWParserMT) + nameof(Dispose) + ":  Disposing");
            this._m = null;
            this._thDic = null;
        }

        
    }
    class MatrixBWParser: IMatrixBWParser<BW> { 
        public MatrixBW Parse(IImage<BW> image) {
            var m = new MatrixBW(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++) {
                for (int y = 0; y < image.Height; y++) {
                    m[x, y] = image[x, y].Intensity;
                }
            }
            return m;
        }
    }

    interface IMatrixBWParser<T> {
        public MatrixBW Parse(IImage<T> image);
    }

    
}
