using OCR_MT.Core;

namespace OCR_MT.Imaging {
    class MatrixBWParser : IMatrixBWParser<BW> {
        public MatrixBW Parse(IImage<BW> image) {
            var m = new MatrixBW(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++) {
                for (int y = 0; y < image.Height; y++) {
                    m[x, y] = image[x, y].Intensity;
                }
            }
            return m;
        }
    }
}
