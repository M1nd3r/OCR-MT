using System.Collections.Generic;
using OCR_MT.Core;
using OCR_MT.Imaging;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.IO {
    class MatrixBWLoader : IMatrixBWLoader {
        IMatrixBWParser<BW> parser;
        public MatrixBWLoader(IMatrixBWParser<BW> parser) {
            this.parser = parser;
        }
        public MatrixBW Load(string path) => parser.Parse(new ImageBWWrapper(SixLabors.ImageSharp.Image.Load<Rgba32>(path)));
        public IEnumerable<MatrixBW> Load(ICollection<string> paths) {
            List<MatrixBW> r = new List<MatrixBW>();
            foreach (var path in paths)
                r.Add(Load(path));
            return r;
        }
    }
}
