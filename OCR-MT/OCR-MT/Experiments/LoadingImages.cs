using System;
using System.Threading.Tasks;
using OCR_MT.Imaging;
using OCR_MT.IO;
using static OCR_MT.Utils.Constants;
using OCR_MT.Core;
using OCR_MT.Logging;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace OCR_MT.Experiments {
    internal static class LoadingImages {
        private static ILogger logger = LoggerFactory.GetLogger();
        public static void TestIS(int pages=50) {
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
            MatrixBW[] images = new MatrixBW[pages];
            t.Start();
            for (int i = 0; i < images.Length; i++) {
                images[i] = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_0" + (i + 10).ToString() + ".png");
            }
            t.Stop();
            Console.WriteLine(t.Elapsed);
            Console.WriteLine("--------------");
        }
        public static void TestIS2(int pages=50) {
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
            MatrixBW[] images = new MatrixBW[pages];
            t.Start();
            for (int i = 0; i < images.Length; i++) {
                images[i] = loader.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_0" + (i + 10).ToString() + ".png");
            }
            t.Stop();
            Console.WriteLine(t.Elapsed);
            Console.WriteLine("--------------");
        }
                
        public static void TestIS_MT3(int pages=50) {
            Console.WriteLine("IS_MT3:");
            MatrixBWLoader_MT<MatrixBWParser_MT2> loader5 = new MatrixBWLoader_MT<MatrixBWParser_MT2>(new MatrixBWParser_MT2());
            Stopwatch t = new Stopwatch();

            t.Start();
            MatrixBW image = loader5.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_010.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Reset();

            t.Start();
            MatrixBWLoader_MT<MatrixBWParser_MT2> loader4 = new MatrixBWLoader_MT<MatrixBWParser_MT2>(new MatrixBWParser_MT2());
            MatrixBW image4 = loader4.Load(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_011.png");
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Reset();
            List<MatrixBW> images;
            List<string> paths = new List<string>();
            for (int i = 0; i < pages; i++) {
                paths.Add(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_0" + (i + 10).ToString() + ".png");
            }
            t.Start();
            images = new List<MatrixBW>(loader5.Load(paths));
            string path = "../../../" +"MT2_parser"+ DateTime.Now.Ticks.ToString() + "/";
            Directory.CreateDirectory(path);
            t.Stop();
            Console.WriteLine(t.Elapsed);
            t.Restart();
            for (int i = 0; i < images.Count; i++) {
                //MatrixBWSaver.SaveMatrixBW(images[i], path + i.ToString() + ".png");
            }
            t.Stop();
            Console.WriteLine(t.Elapsed);
            Console.WriteLine("--------------");
        }
    }
}
