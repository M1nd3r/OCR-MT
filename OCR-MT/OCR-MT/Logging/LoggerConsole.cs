using System;

namespace OCR_MT.Logging {
    class LoggerConsole : ILogger {
        public void Out(string s) => Console.WriteLine(s);
    }
}
