using OCR_MT.Core.Identification;
using OCR_MT.Extraction;
using System;
using System.Collections.Generic;
using System.IO;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.IO {
    class AlphabetLoaderDisc : IAlphabetLoader {
        protected readonly string _path;
        protected readonly IComponentExtractor _componentExtractor;
        protected readonly IImageLoader<byte> _imageLoader;
        protected static int counterID = 1000000;

        public AlphabetLoaderDisc(string path, IComponentExtractor componentExtractor, IImageLoader<byte> imageLoader) {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (componentExtractor == null)
                throw new ArgumentNullException(nameof(componentExtractor));

            _path = path;
            _componentExtractor = componentExtractor;
            _imageLoader = imageLoader;
        }
        public IAlphabet Load() {
            IList<ILetter> letters = new List<ILetter>();
            IEnumerable<string> folders = Directory.EnumerateDirectories(_path);
            foreach (var folder in folders) {
                var files = Directory.EnumerateFiles(folder);                

                foreach (var imageFile in files) {
                    //var xc = Path.GetFileNameWithoutExtension(imageFile);
                    //var xx = _componentExtractor.Extract(_imageLoader.Load(imageFile), Colors.Black_byte, imageFile)[0];
                    letters.Add(
                        new LetterBW(
                            ++counterID,
                            Path.GetFileNameWithoutExtension(imageFile),
                            _componentExtractor.Extract(_imageLoader.Load(imageFile), Colors.Black_byte, imageFile)[0])
                        );
                }
            }
            return new Alphabet(letters);
        }
    }
}
