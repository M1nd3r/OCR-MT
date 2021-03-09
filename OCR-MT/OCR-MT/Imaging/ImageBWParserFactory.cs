using OCR_MT.Core;
namespace OCR_MT.Imaging {
    class ImageBWParserFactory : IThreadSafe {
        private static object _lock = new object();
        public static ImageBWParser GetParser() {
            lock (_lock) return ImageBWParser.GetParser();
        }
    }
}
