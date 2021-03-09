using OCR_MT.Core;

namespace OCR_MT.Imaging {
    class MatrixBWParser : IParser<IImage<byte>, MatrixBW> {
        public MatrixBW Parse(IImage<byte> image) {
            var m = new MatrixBW(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++) {
                for (int y = 0; y < image.Height; y++) {
                    m[x, y] = image[x, y];
                }
            }
            return m;
        }
    }
}
