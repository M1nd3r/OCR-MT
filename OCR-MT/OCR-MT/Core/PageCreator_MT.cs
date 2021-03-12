using OCR_MT.MT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCR_MT.Logging;
using System.Threading;
using OCR_MT.Utils;
using OCR_MT.Imaging;
using System.Runtime;
using OCR_MT.IO;

namespace OCR_MT.Core {
    class PageCreator_MT {
        private static ILogger logger = LoggerFactory.GetLogger();
        private ThreadManager _tm;
        private int _index;
        private IPage<byte>[] _pages;
        private IList<string> _paths;
        private bool[] _inProgress;
        private int _pageStartID;

        private Dictionary<int, int> _thDic;
        private static object
            _lockInit = new object(),
            _lockNextPath = new object(),
            _lockIndex = new object(),
            _lockLoad = new object();

        private int[] _indices;
        private IImage<byte>[] _images;
        private PageFactory_MT[] _pageFactories;


        public PageCreator_MT() {
            _tm = ThreadManager.GetThreadManager();
            _index = 0;
        }
        public IPage<byte>[] Create(ICollection<string> paths) {
            _paths = new List<string>(paths);
            _pages = new IPage<byte>[paths.Count];
            _inProgress = new bool[paths.Count];
            _inProgress.SetAllToFalse();


            int
                size = paths.Count,
                available = _tm.ThreadsAvailable(),
                numberOfThreads = 3;
            if (available >= 4)
                numberOfThreads = available;

            _thDic = new Dictionary<int, int>(numberOfThreads);
            _indices = new int[numberOfThreads];
            _pageFactories = new PageFactory_MT[numberOfThreads];
            _images = new IImage<byte>[numberOfThreads];
            _pageStartID = PageFactory.GetCounter;



            Thread[] pool = new Thread[numberOfThreads];
            for (int i = 0; i < numberOfThreads; i++) {
                pool[i] = _tm.GetNewThread(Task);
                pool[i].Start();
            }
            for (int i = 0; i < numberOfThreads; i++) {
                pool[i].Join();
            }
            return _pages;
        }
        private void Task() {
            logger.Out(nameof(PageCreator_MT) + "." + nameof(Task) + ": Start");
            InitializeThread();

            while (true) {
                logger.Out(nameof(PageCreator_MT) + "." + nameof(Task) + ": Waiting for " + nameof(_lockNextPath));
                lock (_lockNextPath) {
                    logger.Out(nameof(PageCreator_MT) + "." + nameof(Task) + ": " + nameof(_lockNextPath) + " granted");
                    bool isNext = GetNextJob(out _indices[GetIndex()]);
                    if (!isNext) {
                        return;
                    }
                }
                
                _images[GetIndex()] = ImageBWWrapperHandler.Load(_paths[_indices[GetIndex()]]); 


                if (_indices[GetIndex()] % 20 == 0) {
                    GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                    GC.Collect();
                    logger.Out(nameof(PageCreator_MT) + "." + nameof(Task) + " - GC.LOH compacted");
                }
                logger.Out(nameof(PageCreator_MT) + "." + nameof(Task) + ": Next path = " + _indices[GetIndex()].ToString());
                _pages[_indices[GetIndex()]] = _pageFactories[GetIndex()]
                    .Create(_images[GetIndex()], _pageStartID + _indices[GetIndex()]);
            }

        }
        private void InitializeThread() {
            lock (_lockInit) {
                _thDic.Add(Thread.CurrentThread.ManagedThreadId, _index++);
                _pageFactories[GetIndex()] = new PageFactory_MT(); //Hmmm, this could be done better
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
