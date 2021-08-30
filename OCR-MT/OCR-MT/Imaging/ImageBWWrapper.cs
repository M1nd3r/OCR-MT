using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace OCR_MT.Imaging {
    abstract class ImageWrapper<T> {
        protected T _image;
        public ImageWrapper(T image) {
            _image = image;
        }
        internal T GetImage {get=>_image; }
    }
    class ImageBWWrapper : ImageWrapper<Image<Rgba32>>, IImage<byte> {
        public ImageBWWrapper(Image<Rgba32> image) : base(image) { }

        public int Width => _image.Width;
        public int Height => _image.Height;
        public byte this[int x, int y] { get => _image[x, y].R; }

    }
    class ImageBWWrapperHandler : ImageWrapper<Image<Rgba32>> {
        private ImageBWWrapperHandler(Image<Rgba32> image) : base(image) { }
        public static ImageBWWrapper Load(string path) {
            return new ImageBWWrapper(Image.Load<Rgba32>(path));
        }
        public static void Save(ImageBWWrapper image, string path) {
            image.GetImage.Save(path);
        }
    }
}
    

