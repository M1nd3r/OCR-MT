using OCR_MT.Core.Identification;
using OCR_MT.Extraction;
using OCR_MT.Imaging;
using OCR_MT.IO;
using System.IO;
using System.Linq;

namespace OCR_MT.Core.Detection {
    class Analyser {
        private readonly string 
            _pathSource, 
            _pathAlphabet;
        private IAlphabet _alphabet;
        public Analyser(string pathSource, string pathAlphabet) {
            _pathAlphabet = pathAlphabet;
            _pathSource = pathSource;
        }
        public IPage<byte>[] Analyse() {
            _alphabet = GetSampleAlphabet();
            IDocument<byte> inputDocument = GetSampleDocument();
            IPageComposer composer = SetUpPageComposer();
            return composer.Compose(inputDocument).GetPages.ToArray();
        }
        private IPageComposer SetUpPageComposer() {
            ISorter<ILetter> sorter = SetUpPageComponentSorter();
            return 
                new PageComposerFactory<ILetter>().GetPageComposer()
                    .SetSorter(sorter)
                    .SetAlphabet(_alphabet);
        }
        private ISorter<ILetter> SetUpPageComponentSorter() {
            return new PageComponentSorter(_alphabet);            
        }
        private IAlphabet GetSampleAlphabet( ) {
            IComponentExtractor extractor = GetComponentExtractor();
            IImageLoader<byte> imgLoader = GetImageLoader();
            return new AlphabetLoaderDisc(_pathAlphabet, extractor, imgLoader).Load();
        }
        private IComponentExtractor GetComponentExtractor() => new ComponentBWExtractor();
        private IImageLoader<byte> GetImageLoader()=> new ImageBWLoader(ImageBWParserFactory.GetParser());
        private string[] GetSamplePagesPaths() {
            return Directory.GetFiles(_pathSource).Skip(5).Take(5).ToArray();
        }
        private IDocument<byte> GetSampleDocument() {
            IDocumentCreator dc = new DocumentCreator_MT();
            var pagesPaths = GetSamplePagesPaths();
            return dc.Create(pagesPaths);
        }
    }
}
