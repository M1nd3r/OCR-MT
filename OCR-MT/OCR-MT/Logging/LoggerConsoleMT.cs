using System;
using System.Threading;

namespace OCR_MT.Logging {
    class LoggerConsoleMT : ILogger {
        public void Out(string s) {
            Console.WriteLine("Thread ("+Thread.CurrentThread.Name + ") [" + Thread.CurrentThread.ManagedThreadId + "]: " + s);  
        }
    }
}
