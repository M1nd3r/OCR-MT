namespace OCR_MT.Core {
    interface IPageFactory<T> {
        public IPage<T> Create();
    }
}
