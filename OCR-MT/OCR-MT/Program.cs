using System;
using System.Globalization;
using System.Threading;
using OCR_MT.Localisation;
namespace OCR_MT {
    class Program {
        static void Main(string[] args) {
            //SOLID
            //Single Responsibility Principle
            //Open-Close Principle
            //Liskov substitution Principle
            //Interface Segregation Principle
            //Dependency Inversion Principle
           
            Out(Environment.ProcessorCount.ToString());
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("cs");
            Console.WriteLine(Strings.Welcome+", "+Environment.UserName);
        }
        static void Out(string s) {
            Console.WriteLine(s);        
        }
    }
}
