using System;
using OCR_MT.MT;
using OCR_MT.Imaging;
using System.Collections.Generic;
using System.Threading;
using OCR_MT.Core;
using OCR_MT.Utils;
using OCR_MT.Logging;


namespace OCR_MT.IO {
    class MatrixBWLoader_MT<T> : IMatrixBWLoader where T : IParser<IImage<byte>, MatrixBW>, IMultiThread, IDisposable {
        ILogger logger = LoggerFactory.GetLogger();

        private IParser<IImage<byte>, MatrixBW> _parser;
        private ThreadManager _tm;
        private Dictionary<int, int> _thDic;
        private static object
            _lockInit = new object(),
            _lockIndex = new object(),
            _lockNextPath = new object(),
            _lockLoad = new object();
        private int _index;
        private IList<MatrixBW> _loadedMatrices;
         private bool[] _inProgress;
        private IList<string> _paths;
       


        //Thread specific variables
        int[] _indices;
        private IImage<byte>[] _images;

        public MatrixBWLoader_MT(T parserMT) {
            _parser = parserMT;
            _tm = ThreadManager.GetThreadManager();
            _index = 0;
        }

        //Loading single file is not multi thread
        public MatrixBW Load(string path) {
            var loader = MatrixBWLoaderFactory.GetLoader();
            return loader.Load(path);
        }

        public IEnumerable<MatrixBW> Load(ICollection<string> paths) {
            _paths = new List<string>(paths);
            int
                size = paths.Count,
                available = _tm.ThreadsAvailable(),
                numberOfThreads = 3;
            if (available >= 4)
                numberOfThreads = available;
            _loadedMatrices = new MatrixBW[paths.Count];
            _inProgress = new bool[paths.Count];
            _inProgress.SetAllToFalse();

            _thDic = new Dictionary<int, int>(numberOfThreads);
            _indices = new int[numberOfThreads];
            _images = new IImage<byte>[numberOfThreads];

            Thread[] pool = new Thread[numberOfThreads];
            for (int i = 0; i < numberOfThreads; i++) {
                pool[i] = _tm.GetNewThread(Task);
                pool[i].Start();
            }
            for (int i = 0; i < numberOfThreads; i++) {
                pool[i].Join();
            }
            return _loadedMatrices;
            

        }
        private void Task() {
            logger.Out(nameof(MatrixBWLoader_MT<T>) + "." + nameof(Task) + ": Start");
            InitializeThread();
            (_parser as IMultiThread).InitializeThread();
            while (true) {
                logger.Out(nameof(MatrixBWLoader_MT<T>) + "." + nameof(Task) + ": Waiting for " + nameof(_lockNextPath));
                lock (_lockNextPath) {
                    logger.Out(nameof(MatrixBWLoader_MT<T>) + "." + nameof(Task) + ": " + nameof(_lockNextPath) + " granted");

                    bool isNext = GetNextJob(out _indices[GetIndex()]);
                    if (!isNext) {
                        return;
                    }
                    logger.Out(nameof(MatrixBWLoader_MT<T>) + "." + nameof(Task) + ": Next path = "+ _indices[GetIndex()].ToString());

                }
                logger.Out(nameof(MatrixBWLoader_MT<T>) + "." + nameof(Task) + ": Waiting for "+nameof(_lockLoad));

                lock (_lockLoad) {
                    logger.Out(nameof(MatrixBWLoader_MT<T>) + "." + nameof(Task) + ": " + nameof(_lockLoad)+" granted");
                    _images[GetIndex()] =  ImageBWWrapperHandler.Load(_paths[_indices[GetIndex()]]);
                }
                logger.Out(nameof(MatrixBWLoader_MT<T>) + "." + nameof(Task) + ": " + nameof(_lockLoad) + " released");
                _loadedMatrices[_indices[GetIndex()]] = _parser.Parse(_images[GetIndex()]);                
            }            
        }        
        private void InitializeThread() {
            lock (_lockInit) {
                _thDic.Add(Thread.CurrentThread.ManagedThreadId, _index++);
            }
        }
        public bool GetNextJob(out int i) {
            lock (_lockNextPath) {
                i = -1;
                for (int y = 0; y < _inProgress.Length; y++) {
                    if (!_inProgress[y]) {
                        i = y;
                        _inProgress[y] = true;
                        return true;
                    }
                }
                return false;
            }
        }
        private int GetIndex() { lock (_lockIndex) return _thDic.GetValueOrDefault(Thread.CurrentThread.ManagedThreadId); }
    }
}
