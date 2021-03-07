using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.Imaging {
    class ImageBWWrapper : IImage<BW> {
        SixLabors.ImageSharp.Image<Rgba32> _image;
        public ImageBWWrapper(SixLabors.ImageSharp.Image<Rgba32> image) {
            _image = image;
        }
        public int Width => _image.Width;
        public int Height => _image.Height;
        public BW this[int x, int y] { get => new BWWrapper(_image[x, y]); }
    }
}
