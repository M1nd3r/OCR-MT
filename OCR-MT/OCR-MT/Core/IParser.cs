namespace OCR_MT.Core {
    interface IParser<IN, OUT> {
        public OUT Parse(IN item);
    }
}
