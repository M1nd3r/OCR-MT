namespace OCR_MT.Core.Identification {
    class LetterComponentDist : ILetter {
        public ILetter letter;
        public IComponent<byte> component;
        public double distance;
        public LetterComponentDist(ILetter letter, IComponent<byte> component, double distance) {
            this.letter = letter;
            this.component = component;
            this.distance = distance;
        }
        public byte this[int x, int y] => letter[x, y];
        public int Width => letter.Width;
        public int Height => letter.Height;
        public int MaxX => letter.MaxX + component.MinX;
        public int MinX => component.MinX;
        public int MaxY => letter.MaxY + component.MinY;
        public int MinY => component.MinY;
        public float CentroidX => letter.CentroidX;
        public float CentroidY => letter.CentroidY;
        public long Pixels => letter.Pixels;
        public int LetterID => letter.LetterID;
        public string Translation => letter.Translation;
        public IComponent<byte> LetterComponent => letter.LetterComponent;
        public int ComponentID => component.ComponentID;
    }
}


