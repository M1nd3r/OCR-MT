using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OCR_MT.MT {
    class ThreadManager :IThreadManager{
        private static ThreadManager _tm=null;
        private static object _lock=new Object();
        private int _available;
        private List<Thread> _threads;
        private ThreadManager() {
            _tm = this;
            _available = Environment.ProcessorCount-1;
            _threads = new List<Thread>();
        }
        //Singleton pattern
        public static ThreadManager GetThreadManager() {
            lock (_lock) return (_tm == null) ? new ThreadManager() : _tm;
        }
        private void RemoveDeadThreads() {
            lock (_lock) {
                for (int i = _threads.Count-1; i >= 0; i--) {
                    if (!_threads[i].IsAlive)
                        _threads.RemoveAt(i);
                }
            }
        }
        public Thread GetNewThread(ParameterizedThreadStart threadStart) {
            lock (_lock) {
                RemoveDeadThreads();
                Thread t = new Thread(threadStart);                
                _threads.Add(t);
                return t;
            }
        }
        public Thread GetNewThread(ThreadStart threadStart) {
            lock (_lock) {
                RemoveDeadThreads();
                Thread t = new Thread(threadStart);
                _threads.Add(t);
                return t;
            }
        }
        public Thread GetNewThread(ThreadStart threadStart, int maxStackSize) {
            lock (_lock) {
                RemoveDeadThreads();
                Thread t = new Thread(threadStart, maxStackSize);
                _threads.Add(t);
                return t;
            }
        }
        public Thread GetNewThread(ParameterizedThreadStart threadStart, int maxStackSize) {
            lock (_lock) {
                RemoveDeadThreads();
                Thread t = new Thread(threadStart, maxStackSize);
                _threads.Add(t);
                return t;
            }
        }
        public int ThreadsAvailable() {
            lock (_lock) {
                RemoveDeadThreads();
                return (0 <= _available - _threads.Count) ? _available - _threads.Count : 0;
            }
        }        
    }
}
