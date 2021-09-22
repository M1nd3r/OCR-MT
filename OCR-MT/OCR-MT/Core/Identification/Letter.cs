namespace OCR_MT.Core.Identification {
    interface ILetter<T> : IComponent<T> {
        public int LetterID { get; }
        public string Translation { get; }
        public IComponent<T> LetterComponent { get; }
    }
    interface ILetter : ILetter<byte> { }
    class LetterBWFactory {
        private static int counter=0;
        public ILetter Create(string translation, IComponent<byte> component) {
            return new LetterBW(++counter, translation, component);
        }
    }
    internal abstract class Letter<T> : ILetter<T> {
        public Letter(int ID, string translation, IComponent<T> component)
        {
            this.LetterID = ID;
            this.Translation = translation;
            this.LetterComponent = component;
        }
        public int LetterID { get; protected set; }
        public string Translation { get; protected set; }
        public IComponent<T> LetterComponent { get; protected set; }
        public T this[int x, int y] => LetterComponent[x, y];

        public int Width => LetterComponent.Width;
        public int Height => LetterComponent.Height;
        public int MaxX => LetterComponent.MaxX;
        public int MinX => LetterComponent.MinX;
        public int MaxY => LetterComponent.MaxY;
        public int MinY => LetterComponent.MinY;
        public float CentroidX => LetterComponent.CentroidX;
        public float CentroidY => LetterComponent.CentroidY;
        public long Pixels => LetterComponent.Pixels;
        public int ComponentID => LetterComponent.ComponentID;
    }
    class LetterBW : Letter<byte>, ILetter {
        public LetterBW(int ID, string translation, IComponent<byte> component)
            : base(ID, translation, component) { }
    }    
}
