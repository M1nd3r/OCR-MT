using OCR_MT.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.IO {
    class ImageBWRSaver {
        public static void Save(IImage<byte> m, string path) {
            Image<Rgba32> img = new Image<Rgba32>(m.Width, m.Height, Rgba32.ParseHex("ffffffff"));
            for (int x = 0; x < m.Width; x++) {
                for (int y = 0; y < m.Height; y++) {
                    if (m[x, y] == Colors.Black_byte)
                        img[x, y] = Rgba32.ParseHex("000000ff");
                    if (m[x,y]==Colors.Red_byte)
                        img[x, y] = Rgba32.ParseHex("ff0000ff");
                }
            }
            img.Save(path);
        }
    }
}
