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
    class MatrixBWLoaderMT<T> : IMatrixBWLoader where T : IMatrixBWParser<BW>, IMultiThread, IDisposable {
        private static ILogger logger = LoggerFactory.GetLogger();
        private static object _lock = new object();
        private bool[] _open;
        private IMatrixBWParser<BW> _parser;
        private IMultiThread _multiThread;
        private IImage<BW>[] _images;
        private Dictionary<int, int> _thDic;
        public MatrixBWLoaderMT(T parserMT) {
            logger.Out(nameof(MatrixBWLoaderMT<T>) + ".Constructor START");
            _parser = parserMT;
            _multiThread = parserMT;
            _images = new IImage<BW>[parserMT.NumberOfThreads];
            _thDic = new Dictionary<int, int>();
            _open = new bool[parserMT.NumberOfThreads];
            _open.SetAllToTrue();
            logger.Out(nameof(MatrixBWLoaderMT<T>) + ".Constructor END");

        }
        public void InitializeThread() {
            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(InitializeThread) + ": Waiting for lock");

            lock (_lock) {
                logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(InitializeThread) + ": Lock granted");
                if (!_thDic.ContainsKey(Thread.CurrentThread.ManagedThreadId)) {
                    while (true) {
                        logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(InitializeThread) + ": Trying to obtain open slot");
                        var openIndex = GetOpened();
                        if (openIndex > 0)
                            _thDic.Add(Thread.CurrentThread.ManagedThreadId, openIndex);
                        else {
                            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(InitializeThread) + ":Thread blocked - no empty slot");
                            Thread.Sleep(0);
                        }
                    }
                }
                logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(InitializeThread) + ": Lock to be released");
            }
            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(InitializeThread) + ": Lock released");

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
        private int GetIndex => _thDic.GetValueOrDefault(Thread.CurrentThread.ManagedThreadId);

        public MatrixBW Load(string path) {
            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(Load) + ": To be initialized_1");
            InitializeThread();
            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(Load) + ": To be initialized_2");
            _multiThread.InitializeThread();
            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(Load) + ": Initialized, waiting for lock");
            lock (_lock) {
                logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(Load) + ": Lock granted");
                _images[GetIndex] = new ImageBWWrapper(SixLabors.ImageSharp.Image.Load<Rgba32>(path));
                logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(Load) + ": Lock to be released");
            }
            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(Load) + ": Lock released");
            return _parser.Parse(_images[GetIndex]);
        }

        public void Dispose() {
            logger.Out(nameof(MatrixBWLoaderMT<T>) + nameof(Dispose) + ":  Disposing");
            this._images = null;
            this._parser = null;
            this._multiThread = null;
            this._thDic = null;
        }
    }
}
