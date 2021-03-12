using System.Collections.Generic;
using OCR_MT.Core;
using OCR_MT.Imaging;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.IO {
    class MatrixBWLoaderIS:IMatrixBWLoader {
        public MatrixBW Load(string path) {
            MatrixBW m;
            using (SixLabors.ImageSharp.Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(path)) {
                m = new MatrixBW(image.Width, image.Height);
                for (int x = 0; x < m.Width; x++) {
                    for (int y = 0; y < m.Height; y++) {
                        m[x, y] = (image[x, y].R > 0) ? (byte)0 : (byte)1;
                        var a = image[x, y];
                    }
                }
            }
            return m;
        }
        public IEnumerable<MatrixBW> Load(ICollection<string> paths) {
            List<MatrixBW> r = new List<MatrixBW>();
            foreach (var path in paths)
                r.Add(Load(path));
            return r;
        }
    }

    
    

    
   
}
