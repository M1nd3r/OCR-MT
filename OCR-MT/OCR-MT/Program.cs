using System;
using System.Globalization;
using System.Threading;
using OCR_MT.Localisation;
using OCR_MT.Experiments;
using OCR_MT.MT;
using OCR_MT.Logging;

namespace OCR_MT {
    class Program {
        private static ILogger logger = LoggerFactory.GetLogger();
        static void Main(string[] args) {
            //SOLID
            //Single Responsibility Principle
            //Open-Close Principle
            //Liskov substitution Principle
            //Interface Segregation Principle
            //Dependency Inversion Principle
            ThreadManager tm = ThreadManager.GetThreadManager();
            logger.Out(tm.ThreadsAvailable().ToString());
            LoadingImages.TestIS_MT3(500);
            LoadingImages.TestIS();
            LoadingImages.TestIS2();

            Out(Environment.ProcessorCount.ToString());
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("cs");
            Console.WriteLine(Strings.Welcome + ", " + Environment.UserName);
        }
        static void Out(string s) {
            Console.WriteLine(s);
        }
    }
}
