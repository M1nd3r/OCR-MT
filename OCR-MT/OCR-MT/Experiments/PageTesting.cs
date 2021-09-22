using OCR_MT.Core;
using OCR_MT.IO;
using OCR_MT.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.Experiments {
    class PageTesting {
        private static ILogger logger = LoggerFactory.GetLogger();
        public static void CreateAndSave(int min = 10, int max = 20) {
            Directory.CreateDirectory(Paths.Experiments.OutputPages);
            Directory.CreateDirectory(Paths.Experiments.InputPages);
            List<string> paths = new List<string>();
            paths = Paths.Experiments.Jakobei.Get(min, max);
            var imageLoader = ImageLoaderFactory.GetLoader_byte();

            Directory.CreateDirectory(Paths.Experiments.OutputPages);
            Stopwatch t = new Stopwatch();

            t.Start();
            IPage<byte>[]  pages = new IPage<byte>[paths.Count];

            for (int i = 0; i < pages.Length; i++) {
                logger.Out(nameof(PageTesting) + "." + nameof(CreateAndSave) + " - Creating page: " + (i + 1) + " / " + pages.Length);
                var pagefactory = new PageFactory(imageLoader.Load(paths[i]));
                pages[i] = pagefactory.Create();
                logger.Out(nameof(PageTesting) + "." + nameof(CreateAndSave) + " - Saving page: " + (i + 1) + " / " + pages.Length);
                PageSaverFactory.Get().Save(pages[i], Paths.Experiments.OutputPages + pages[i].ID + "_reconstructed.png");
            }
            t.Stop();
            logger.Out(nameof(PageTesting) + "." + nameof(CreateAndSave) + " - Total time: " + t.Elapsed);

        }
        public static void CreateAndSave_MT(int min = 10, int max = 20) {
            Directory.CreateDirectory(Paths.Experiments.OutputPages);
            Directory.CreateDirectory(Paths.Experiments.InputPages);
            List<string> paths = new List<string>();
            paths = Paths.Experiments.Jakobei.Get(min, max);
            //var pagefactory = new PageFactory();
            var imageLoader = ImageLoaderFactory.GetLoader_byte();

            IPage<byte>[] pages = new IPage<byte>[paths.Count];
            Directory.CreateDirectory(Paths.Experiments.OutputPages);
            Stopwatch t = new Stopwatch();
            t.Start();
            logger.Out(nameof(PageTesting) + "." + nameof(CreateAndSave) + " - Starting multithread creation");
            PageCreator_MT pcmt = new PageCreator_MT();
            pages = pcmt.Create(paths);

            t.Stop();
            logger.Out(nameof(PageTesting) + "." + nameof(CreateAndSave) + " - Total time: " + t.Elapsed);
            /**/
            Parallel.For(0, pages.Length, i => {
                logger.Out(nameof(PageTesting) + "." + nameof(CreateAndSave) + " - Saving page: " + (i + 1) + " / " + pages.Length);
                PageSaverFactory.Get().Save(pages[i], Paths.Experiments.OutputPages + pages[i].ID + "_reconstructed_MT.png");
            });
            t.Stop();
            logger.Out(nameof(PageTesting) + "." + nameof(CreateAndSave) + " - Total time: " + t.Elapsed);
            /**/
        }      
    }
}
