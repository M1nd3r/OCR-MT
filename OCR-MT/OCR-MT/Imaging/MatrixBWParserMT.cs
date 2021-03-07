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

namespace OCR_MT.Imaging {
    class MatrixBWParserMT : IMatrixBWParser<BW>, IMultiThread, IDisposable {
        private ILogger logger = LoggerFactory.GetLogger();
        private bool[] _open;
        private static object _lock = new object();
        private int[] _x;
        private int[] _y;
        private MatrixBW[] _m;
        private Dictionary<int, int> _thDic;
        public int NumberOfThreads { get; }
        public MatrixBWParserMT(int numberOfThreads) {
            logger.Out(nameof(MatrixBWParserMT) + ".Constructor" + ":  Start nt=" + numberOfThreads.ToString() + ")");
            _x = new int[numberOfThreads];
            _y = new int[numberOfThreads];
            _thDic = new Dictionary<int, int>();
            _open = new bool[numberOfThreads];
            _open.SetAllToTrue();
            _m = new MatrixBW[numberOfThreads];
            this.NumberOfThreads = numberOfThreads;
        }
        public void InitializeThread() {
            logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ":  Waiting for lock");
            lock (_lock) {
                logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Lock granted");
                if (!_thDic.ContainsKey(Thread.CurrentThread.ManagedThreadId)) {
                    while (true) {
                        logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Trying to obtain open slot");
                        var openIndex = GetOpened();
                        if (openIndex > 0)
                            _thDic.Add(Thread.CurrentThread.ManagedThreadId, openIndex);
                        else {
                            logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ":Thread blocked - no empty slot");
                            Thread.Sleep(0);
                        }
                    }
                }
                logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Added thread to dictionary, new n=" + _thDic.Count);
                logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Lock to be released");
            }
            logger.Out(nameof(MatrixBWParserMT) + nameof(InitializeThread) + ": Lock released");
        }
        public int GetOpened() {
            for (int i = 0; i < _open.Length; i++) {
                if (_open[i]) {
                    _open[i] = false;
                    return i;
                }
            }
            return -1;
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
}
