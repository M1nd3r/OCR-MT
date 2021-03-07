
using System.Threading;

namespace OCR_MT.MT {
    interface IThreadManager {
        public int ThreadsAvailable();
        public Thread GetNewThread(ParameterizedThreadStart threadStart);
        public Thread GetNewThread(ThreadStart threadStart);
        public Thread GetNewThread(ThreadStart threadStart, int maxStackSize);
        public Thread GetNewThread(ParameterizedThreadStart threadStart, int maxStackSize);
    }
}
