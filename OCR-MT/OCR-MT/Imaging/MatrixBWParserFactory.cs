using OCR_MT.Core;
namespace OCR_MT.Imaging {
    class MatrixBWParserFactory {
        public static IParser<IImage<byte>, MatrixBW> GetParser() => new MatrixBWParser();        
        public static IParser<IImage<byte>, MatrixBW> GetParserMT() => new MatrixBWParser_MT();
    }
}
