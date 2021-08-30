using OCR_MT.Core;
using OCR_MT.Core.Identification;
using OCR_MT.Extraction;
using OCR_MT.Imaging;
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
        private IList<ILetter> _letters;
        private IEnumerable<string> _files;

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
            _letters = new List<ILetter>();
            IEnumerable<string> folders = Directory.EnumerateDirectories(_path);
            foreach (var folder in folders) {
                _files = Directory.EnumerateFiles(folder);
                AddLetters();                
            }
            var letters = _letters;
            CleanAfterLoad();
            return new Alphabet(letters);
        }

        private void AddLetters() {
            foreach (var imageFile in _files) {
                _letters.Add(GetLetterFromImage(imageFile));                    
            }
        }
        private ILetter GetLetterFromImage(string imageFile) {
            string name = Path.GetFileNameWithoutExtension(imageFile);
            IImage<byte> loadedImage = _imageLoader.Load(imageFile);
            IComponent<byte> component = _componentExtractor.Extract(loadedImage, Colors.Black_byte, imageFile)[0];
            return new LetterBW(++counterID, name, component);                                           
        }

        private void CleanAfterLoad() {
            SetLettersToNull();
            SetFilesToNull();
        }
        private void SetLettersToNull() {
            _letters = null;
        }
        private void SetFilesToNull() {
            _files = null;
        }
    }
}
