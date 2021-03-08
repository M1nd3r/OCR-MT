namespace OCR_MT.Imaging {
    class MatrixBWParserFactory {
        public static IMatrixBWParser<byte> GetParser() => new MatrixBWParser();        
        public static IMatrixBWParser<byte> GetParserMT() => new MatrixBWParser_MT();
    }
}
