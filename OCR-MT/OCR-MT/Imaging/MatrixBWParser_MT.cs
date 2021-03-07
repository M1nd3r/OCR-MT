using System;
using OCR_MT.MT;
using OCR_MT.Imaging;
using System.Collections.Generic;
using System.Threading;
using OCR_MT.Core;
using OCR_MT.Logging;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.Imaging {
    class MatrixBWParser_MT : IMatrixBWParser<BW>, IMultiThread,IDisposable {
        private ILogger logger = LoggerFactory.GetLogger();

        private static object
            _lockInit = new object(),
            _lockIndex = new object(); //Hopefully not needed:  vý-vyl-sý
        private List<int>
            _x,
            _y;
        private List<MatrixBW> _m;
        private Dictionary<int, int> _thDic;
        private int _index;
        public MatrixBWParser_MT() {
            _index = 0;
            _x = new List<int>();
            _y = new List<int>();
            _m = new List<MatrixBW>();
            _thDic = new Dictionary<int, int>();
        }
        public int NumberOfThreads => _index;

        public void InitializeThread() {
            lock (_lockInit) {
                //Adding arbitrary values to thread-specific lists to make them bigger
                _x.Add(0);
                _y.Add(0);
                _m.Add(null);
                _thDic.Add(Thread.CurrentThread.ManagedThreadId, _index++);
            }
        }

        public MatrixBW Parse(IImage<BW> image) {
            logger.Out(nameof(MatrixBWParser_MT) + "." + nameof(Parse) + ": Start");
            _m[GetIndex] = new MatrixBW(image.Width, image.Height);
            for (_x[GetIndex] = 0; _x[GetIndex] < image.Width; _x[GetIndex]++) {
                for (_y[GetIndex] = 0; _y[GetIndex] < image.Height; _y[GetIndex]++) {
                    _m[GetIndex][_x[GetIndex], _y[GetIndex]] = image[_x[GetIndex], _y[GetIndex]].Intensity;
                }
            }
            return _m[GetIndex];
        }
        private int GetIndex { get => _thDic.GetValueOrDefault(Thread.CurrentThread.ManagedThreadId); }
        public void Dispose() {
            logger.Out(nameof(MatrixBWParserMT) + nameof(Dispose) + ":  Disposing");
            this._m = null;
            this._thDic = null;
        }
    }
}
