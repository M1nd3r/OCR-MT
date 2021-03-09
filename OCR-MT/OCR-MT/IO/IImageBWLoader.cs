using System;
using System.Collections.Generic;
using OCR_MT.Imaging;

namespace OCR_MT.IO {
    interface IImageLoader<T> {
        public IImage<T> Load(string path);
        public IEnumerable<IImage<T>> Load(ICollection<string> paths);
    }
}
