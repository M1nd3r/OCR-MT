using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCR_MT.Core;
using OCR_MT.IO;
using OCR_MT.Extraction;
using OCR_MT.Imaging;
using static OCR_MT.Utils.Constants;
using System.IO;
using System.Diagnostics;

namespace OCR_MT.Experiments {
    internal static class ExtractingComponents {
        internal static void TestCreation(int pages = 3) {
            IComponentExtractor ce = new ComponentBWExtractor(); //Extractor
            Stopwatch t = new Stopwatch();
            IImageLoader<byte> loader5 = new ImageBWLoader(ImageBWParserFactory.GetParser());
            IList<IImage<byte>> images;
            IList<string> paths = new List<string>();
            paths.Add(Paths.Experiments.Input + "b3.png");
            for (int i = 0; i < pages; i++) {
                var s = "";
                if (i + 10 < 100)
                    s = "0";
                paths.Add(@"D:\MFF\04-LS-2\C#\OCR\images\JakobeiBW\Jakobei_Convert_" + s + (i + 10).ToString() + ".png");
            }
            t.Start();

            images = new List<IImage<byte>>(loader5.Load(paths));
            var a = ce.Extract(images[1], Colors.Black_byte, Path.GetFileName(paths[1])); //Extract black components from image 

            t.Stop();
            Console.WriteLine(t.Elapsed);
            Console.WriteLine(a.Count);
            Console.WriteLine(a.ToString());
            // IComponent<byte> c =new ComponentBW_byte;
        }
    }
}
