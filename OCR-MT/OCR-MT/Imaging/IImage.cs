namespace OCR_MT.Imaging {
    interface IImage<T> {
        public int Width { get; }
        public int Height { get; }
        public T this[int x, int y] { get; }
    }
}
