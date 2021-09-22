namespace OCR_MT.Core.Identification {
    class PageComposerFactory<T> where T:ILetter {
        public APageComposerBase<T> GetPageComposer() {
            return new PageComposer<T>();
        }
    }
}
