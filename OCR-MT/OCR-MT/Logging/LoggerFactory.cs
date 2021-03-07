using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Logging {
    internal static class LoggerFactory {
        public static ILogger GetLogger() => new LoggerConsoleMT();
    }
}
