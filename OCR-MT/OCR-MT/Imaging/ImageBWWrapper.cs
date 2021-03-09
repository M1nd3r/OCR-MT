using SixLabors.ImageSharp.PixelFormats;
using SixLabors;

namespace OCR_MT.Imaging {
    class ImageBWWrapper : IImage<byte> {
        SixLabors.ImageSharp.Image<Rgba32> _image;
        public ImageBWWrapper(SixLabors.ImageSharp.Image<Rgba32> image) {
            _image = image;
        }
        public int Width => _image.Width;
        public int Height => _image.Height;
        public byte this[int x, int y] { get => _image[x, y].R; }
        public static ImageBWWrapper Load(string path)
            => new ImageBWWrapper(SixLabors.ImageSharp.Image.Load<Rgba32>(path));
    }
}
