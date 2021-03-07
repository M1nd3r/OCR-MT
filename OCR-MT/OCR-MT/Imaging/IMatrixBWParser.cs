using OCR_MT.Core;

namespace OCR_MT.Imaging {
    interface IMatrixBWParser<T> {
        public MatrixBW Parse(IImage<T> image);
    }
}
