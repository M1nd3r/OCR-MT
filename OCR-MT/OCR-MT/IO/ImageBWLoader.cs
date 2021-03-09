using System.Collections.Generic;
using OCR_MT.Imaging;
using OCR_MT.Logging;
using OCR_MT.Core;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace OCR_MT.IO {
    class ImageBWLoader : IImageLoader<byte> {
        ILogger logger = LoggerFactory.GetLogger();
        IParser<Image<Rgba32>, IImage<byte>> parser;
        public ImageBWLoader(IParser<Image<Rgba32>,IImage<byte>> parser) {
            this.parser = parser;
        }
        public IImage<byte> Load(string path) => parser.Parse(Image.Load<Rgba32>(path));
        public IEnumerable<IImage<byte>> Load(ICollection<string> paths) {
            List<IImage<byte>> r = new List<IImage<byte>>();
            int i = 0;
            foreach (var path in paths) {
                logger.Out(nameof(ImageBWLoader) + "." + nameof(Load) + ": Loading image " + (++i).ToString() + " / " + paths.Count);
                r.Add(Load(path));
            }
            return r;
        }
    }
}
