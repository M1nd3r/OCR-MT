using System;
using System.Collections.Generic;
using System.Text;
using OCR_MT.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace OCR_MT.IO {
    class MatrixBWLoaderIS:IMatrixBWLoader {
        public MatrixBW Load(string path) {
            MatrixBW m;
            using (Image<Rgba32> image = Image.Load<Rgba32>(path)) {
                m = new MatrixBW(image.Width, image.Height);
                for (int x = 0; x < m.Width; x++) {
                    for (int y = 0; y < m.Height; y++) {
                        m[x, y] = (image[x, y].R > 0) ? (byte)0 : (byte)1;
                    }
                }
            }
            return m;
        }
    }
}
