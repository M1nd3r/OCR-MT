namespace OCR_MT.Imaging {
    class MatrixBWParserFactory {
        public static IMatrixBWParser<BW> GetParser() => new MatrixBWParser();        
        public static IMatrixBWParser<BW> GetParserMT() => new MatrixBWParser_MT();
    }
}
