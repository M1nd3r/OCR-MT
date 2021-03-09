using OCR_MT.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.Imaging {
    //Singleton
    class ImageBWParser : IParser<Image<Rgba32>, IImage<byte>> {
        private static ImageBWParser parser=null;
        private ImageBWParser() {
        }
        public static ImageBWParser GetParser() {
            if (parser == null)
                parser = new ImageBWParser();
            return parser;
        }            
        public IImage<byte> Parse(Image<Rgba32> image) => new ImageBWWrapper(image);        
    }
}
