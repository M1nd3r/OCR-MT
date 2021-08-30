using System.Collections.Generic;
using OCR_MT.Imaging;
using OCR_MT.Logging;
using OCR_MT.Core;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace OCR_MT.IO {
    class ImageBWLoader_TS : IImageLoader<byte>, IThreadSafe {
        ILogger logger = LoggerFactory.GetLogger();
        IParser<Image<Rgba32>, IImage<byte>> parser;
        private static object _lock = new object();
        public ImageBWLoader_TS(IParser<Image<Rgba32>, IImage<byte>> parser) {
            this.parser = parser;
        }
        public IImage<byte> Load(string path) { 
            lock (_lock) return parser.Parse(Image.Load<Rgba32>(path));
        }
        public IEnumerable<IImage<byte>> Load(ICollection<string> paths) {
            lock (_lock) {
                List<IImage<byte>> r = new List<IImage<byte>>();
                int counter = 0;
                foreach (var path in paths) {
                    logger.Out(nameof(ImageBWLoader_TS) + "." + nameof(Load) + ": Loading image " + (++counter).ToString() + " / " + paths.Count);
                    r.Add(Load(path));
                }
                return r;
            }
        }
    }
}
