using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCR_MT.Core;
using OCR_MT.Imaging;

namespace OCR_MT.IO {
    class ImageLoaderFactory : IThreadSafe {
        private static object _lock = new object();
        private static ImageBWLoader _imageBWLoader = null;
        public static IImageLoader<byte> GetLoader_byte() {
            lock (_lock) {
                if (_imageBWLoader == null)
                    _imageBWLoader = new ImageBWLoader(ImageBWParserFactory.GetParser());
                return _imageBWLoader;
            }
        }
    }
}
