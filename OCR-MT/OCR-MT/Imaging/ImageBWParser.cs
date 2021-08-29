using OCR_MT.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.Imaging {
    //Singleton
    /// <summary>
    /// Singleton Image parser that converts ImageSharp type Image<Rgba32> to OCR_MT type IImage<byte> 
    /// </summary>
    class ImageBWParser : IParser<Image<Rgba32>, IImage<byte>> {
        private static ImageBWParser parser=null;
        private ImageBWParser() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Singleton instance of ImageBWParser</returns>
        public static ImageBWParser GetParser() {
            if (parser == null)
                parser = new ImageBWParser();
            return parser;
        }
        /// <summary>
        /// Parse image from ImageSharp type Image<Rgba32> to OCR_MT type IImage<byte> 
        /// </summary>
        /// <param name="image"> Input image</param>
        /// <returns>Wrapped image</returns>
        public IImage<byte> Parse(Image<Rgba32> image) => new ImageBWWrapper(image);        
    }
}
