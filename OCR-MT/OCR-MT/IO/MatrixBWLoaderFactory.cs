using OCR_MT.Imaging;

namespace OCR_MT.IO {
    class MatrixBWLoaderFactory {
        public static IMatrixBWLoader GetLoader() => new MatrixBWLoader(MatrixBWParserFactory.GetParser());
    }
}
