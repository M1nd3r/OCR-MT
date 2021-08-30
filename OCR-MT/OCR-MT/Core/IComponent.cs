namespace OCR_MT.Core {
    internal interface IComponent {
        public int ComponentID { get; }
        public int Width { get; }
        public int Height { get; }
        public int MaxX { get; }
        public int MinX { get; }
        public int MaxY { get; }
        public int MinY { get; }
        public float CentroidX { get; }
        public float CentroidY { get; }
        public long Pixels { get; }
    }
    internal interface IComponent<T>:IComponent {
        public T this[int x, int y] { get; }
    }
}
