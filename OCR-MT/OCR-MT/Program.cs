using System;
using System.Globalization;
using System.Threading;
using OCR_MT.Localisation;
using OCR_MT.Experiments;
using OCR_MT.MT;
using OCR_MT.Logging;
using System.IO;

namespace OCR_MT {
    class Program {
        private static ILogger logger = LoggerFactory.GetLogger();
        static void Main(string[] args) {

            // SOLID
            // Single Responsibility Principle
            // Open-Close Principle
            // Liskov substitution Principle
            // Interface Segregation Principle
            // Dependency Inversion Principle

            WriteAlphabet();

            ThreadManager tm = ThreadManager.GetThreadManager();
            logger.Out(tm.ThreadsAvailable().ToString());

            CreateAnalyseSave.Run();            
        }
        private static void WriteAlphabet() {
            var folders = Directory.EnumerateDirectories(@"D:\GitHub\OCR-MT\abeceda\abeceda_Verne\");

            foreach (var folder in folders) {
                Console.WriteLine(folder);
                var files = Directory.EnumerateFiles(folder);
                foreach (var imageFile in files) {
                    Console.WriteLine("___," + imageFile);
                }
            }
        }
        private static void NotUsed() {
             PageTesting.CreateAndSave_MT(10,509);
            
            ExtractingComponents.TestCreation();
            LoadingImages.TestIS_ST(50);

            LoadingImages.TestIS2(50);

            LoadingImages.TestIS_MT3(50);

            LoadingImages.TestIS(50);

            logger.Out(Environment.ProcessorCount.ToString());
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("cs");
            Console.WriteLine(Strings.Welcome + ", " + Environment.UserName);
            
        }
    }
}
