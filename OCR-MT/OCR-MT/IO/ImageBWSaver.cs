using OCR_MT.Core;
using OCR_MT.Imaging;
using static OCR_MT.Utils.Constants;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.IO {
    class ImageBWSaver {
        public static void Save(IImage<byte> m,string path) {
            Image<Rgba32> img = new Image<Rgba32>(m.Width, m.Height, Rgba32.ParseHex("ffffffff"));
            for (int x = 0; x < m.Width; x++) {
                for (int y = 0; y < m.Height; y++) {
                    if (m[x, y] == Colors.Black_byte)
                        img[x, y] = Rgba32.ParseHex("000000ff");
                }
            }
            img.Save(path);
        }
        public static void Save(ImageBWWrapper img, string path) {            
            ImageBWWrapperHandler.Save(img,path);
        }
    }
}
