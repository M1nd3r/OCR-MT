namespace OCR_MT.MT {
    interface IMultiThread {
        public void InitializeThread();
        public int NumberOfThreads { get; }
    }
}
