using OCR_MT.Core.Identification;
using OCR_MT.Extraction;
using OCR_MT.Imaging;
using OCR_MT.IO;
using System.IO;
using System.Linq;

using static OCR_MT.Core.Metrics.LettersComponents;

namespace OCR_MT.Core.Detection {
    /// <summary>
    /// Main analysing class that puts together all code components
    /// </summary>
    class Analyser {
        private readonly string _pathSource, _pathAlphabet;
        public Analyser(string pathSource, string pathAlphabet) {
            _pathAlphabet = pathAlphabet;
            _pathSource = pathSource;
        }
        public IPage<byte>[] Analyse() {
            IComponentExtractor ce = new ComponentBWExtractor();
            IImageLoader<byte> imgLoader = new ImageBWLoader(ImageBWParserFactory.GetParser());
            IAlphabet alphabet = new AlphabetLoaderDisc(_pathAlphabet, ce, imgLoader).Load();
            IPageCreator pc = new PageCreator_MT();

            var x = Directory.GetFiles(_pathSource).Skip(5).Take(5).ToArray();

            IPage<byte>[] inputPages = pc.Create(Directory.GetFiles(_pathSource).Skip(5).Take(5).ToArray());
            IPage<byte>[] outputPages = new IPage<byte>[inputPages.Length];
            IComponentSortHandler handler = new ComponentSortHandlerDummy(SizeMetric);
            ISorterPageSetable sorter = new PageComponentSorter(alphabet, null, handler);
            IComposer composer = new PageComposer(sorter);

            for (int i = 0; i < inputPages.Length; i++) {
                sorter.SetPage(in inputPages[i]);
                outputPages[i] = composer.Compose();
            }
            return outputPages;
        }
    }
}
