using System;
using System.Threading.Tasks;
using OCR_MT.Imaging;
using OCR_MT.IO;
using static OCR_MT.Utils.Constants;
using OCR_MT.Core;
using OCR_MT.Logging;
using System.Diagnostics;

namespace OCR_MT.Experiments {
    internal static class LoadingImages {
        private static ILogger logger = LoggerFactory.GetLogger();
        public static void TestIS() {
            Console.WriteLine("IS:");
            MatrixBWLoader loader = new MatrixBWLoader(new MatrixBWParser());
            Stopwatch t = new Stopwatch();
            t.Start();
            MatrixBW image = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_010.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Reset();
            t.Start();
            MatrixBWLoader loader2 = new MatrixBWLoader(new MatrixBWParser());            
            MatrixBW image2 = loader2.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_011.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            MatrixBW[] images = new MatrixBW[50];
            t.Start();
            for (int i = 0; i < images.Length; i++) {
                images[i] = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_0" + (i + 10).ToString() + ".png");
            }
            t.Stop();
            Console.WriteLine(t.Elapsed);
            Console.WriteLine("--------------");
        }
        public static void TestIS2() {
            Console.WriteLine("IS2:");
            MatrixBWLoaderIS loader = new MatrixBWLoaderIS();
            Stopwatch t = new Stopwatch();
            t.Start();
            MatrixBW image = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_010.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Reset();
            t.Start();
            MatrixBWLoaderIS loader2 = new MatrixBWLoaderIS();            
            MatrixBW image2 = loader2.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_011.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Reset();
            MatrixBW[] images = new MatrixBW[50];
            t.Start();
            for (int i = 0; i < images.Length; i++) {
                images[i] = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_0" + (i + 10).ToString() + ".png");
            }
            t.Stop();
            Console.WriteLine(t.Elapsed);
            Console.WriteLine("--------------");
        }
        public static void TestIS_MT() {
            Console.WriteLine("IS_MT:");
            MatrixBWLoaderMT<MatrixBWParserMT> loader = new MatrixBWLoaderMT<MatrixBWParserMT>(new MatrixBWParserMT(10));
            Stopwatch t = new Stopwatch();

            t.Start();
            MatrixBW image = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_010.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Reset();

            t.Start();
            MatrixBWLoaderMT<MatrixBWParserMT> loader3 = new MatrixBWLoaderMT<MatrixBWParserMT>(new MatrixBWParserMT(10)); 
            MatrixBW image3 = loader3.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_011.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Reset();
            MatrixBW[] images = new MatrixBW[50];
            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = 10;
            int alfa = 0;
            t.Start();
            Parallel.For(0, 50,po, i => {
                logger.Out(nameof(TestIS_MT)+"- Next cycle: " + i.ToString());
                images[i] = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_0" + (i + 10).ToString() + ".png");
                logger.Out((alfa++).ToString());
            });
            t.Stop();
            Console.WriteLine(t.Elapsed);
            Console.WriteLine("--------------");
        }



    }
}
