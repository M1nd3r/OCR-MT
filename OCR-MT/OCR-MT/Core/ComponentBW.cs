using OCR_MT.Imaging;
using System.Collections.Generic;
using static OCR_MT.Utils.Constants;
using static OCR_MT.Utils.Extensions;

namespace OCR_MT.Core {
    internal abstract class ComponentBW<T> : IComponent<T>, IImage<T> {
        protected IList<(int X, int Y)> _coords;
        public ComponentBW(int ID) {
            _coords = new List<(int X, int Y)>();
            this.ComponentID = ID;

            //The following initializations are necessary for proper boundary setting
            MinX = int.MaxValue;
            MinY = int.MaxValue;
        }
        public ComponentBW(IList<(int X, int Y)> coords, int ID) : this(ID) {
            _coords = coords;
            Pixels = coords.Count;
            SetBoundaries();
        }

        public abstract T this[int x, int y] { get; protected set; }

        public int ComponentID { get; }

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public int MaxX { get; protected set; }
        public int MinX { get; protected set; }
        public int MaxY { get; protected set; }
        public int MinY { get; protected set; }
        public float CentroidX { get; protected set; }
        public float CentroidY { get; protected set; }
        public long Pixels { get; protected set; }
        public virtual void Finish() {
            SetHeight();
            SetWidth();
            SetCentroidCoordinates();
            SetMatrix();
            ClearCoordinates();
        }
        protected abstract void SetMatrix();
        
        protected void SetCentroidCoordinates() {
            long
                sumX = 0,
                sumY = 0;
            foreach (var (X, Y) in _coords) {
                sumX += X;
                sumY += Y;
            }
            CentroidX = (sumX / Pixels) - MinX;
            CentroidY = (sumY / Pixels) - MinY;
        }
        protected void SetWidth() {
            Width = (MaxX - MinX + 1);
        }
        protected void SetHeight() {
            Height = (MaxY - MinY + 1);
        }
        protected void ClearCoordinates() {
            _coords.Clear();
        }
        protected void SetBoundaries() {
            foreach (var (X, Y) in _coords) {
                if (X < MinX)
                    MinX = X;
                if (X > MaxX)
                    MaxX = X;
                if (Y < MinY)
                    MinY = Y;
                if (Y > MaxY)
                    MaxY = Y;
            }
        }
    }
    internal class ComponentBW_byte : ComponentBW<byte> {

        public ComponentBW_byte(IList<(int, int)> list, int ID) : base(list, ID) {
            Finish();
        }
        public MatrixBW Matrix { get; protected set; }

        public override byte this[int x, int y] { 
            get => Matrix[x, y]; 
            protected set => Matrix[x, y] = value; 
        }
        protected override void SetMatrix() {
            var m = new MatrixBW(Width, Height);
            m.SetAllToMax();
            foreach (var (X, Y) in _coords) {
                m[X - MinX, Y - MinY] = Colors.Black_byte;
            }
            Matrix = m;
        }
    }
    internal class ComponentBW_bit : ComponentBW<byte> {

        public ComponentBW_bit(IList<(int, int)> list, int ID) : base(list, ID) {
            Finish();
        }
        public MatrixBit Matrix { get; protected set; }
        public override byte this[int x, int y] { 
            get => (Matrix[x, y]) ? Colors.Black_byte : Colors.White_byte; 
            protected set => Matrix[x, y] = (value == Colors.Black_byte); 
        }
        protected override void SetMatrix() {
            var m = new MatrixBit(Width, Height);
            foreach (var c in _coords) {
                m[c.X - MinX, c.Y - MinY] = true;
            }
            Matrix = m;
        }
    }
}
