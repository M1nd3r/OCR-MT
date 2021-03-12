using OCR_MT.Imaging;
using System;
using System.Collections.Generic;
using static OCR_MT.Utils.Constants;
using static OCR_MT.Utils.Extensions;

namespace OCR_MT.Core {
    internal abstract class ComponentBW<T> : IComponent<T>, IImage<T> {
        protected IList<(int X, int Y)> _coords;
        public ComponentBW(int ID) {
            _coords = new List<(int X, int Y)>();
            this.ID = ID;
            this.MinX = int.MaxValue;
            this.MinY = int.MaxValue;
            
        }
        public ComponentBW(Queue<(int, int)> q, int ID):this(ID) {
            while (q.Count > 0)
                AddPixel(q.Dequeue());
        }
        public ComponentBW(IList<(int X, int Y)> coords, int ID) : this(ID) {
            _coords = coords;
            Pixels = coords.Count;
            foreach (var coord in coords) {
                if (coord.X < MinX)
                    MinX = coord.X;
                if (coord.X > MaxX)
                    MaxX = coord.X;
                if (coord.Y < MinY)
                    MinY = coord.Y;
                if (coord.Y > MaxY)
                    MaxY = coord.Y;
            }
        }

        public abstract T this[int x, int y] { get; protected set; }

        public int ID { get; }

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public int MaxX { get; protected set; }
        public int MinX { get; protected set; }
        public int MaxY { get; protected set; }
        public int MinY { get; protected set; }
        public float CentroidX { get; protected set; }
        public float CentroidY { get; protected set; }
        public long Pixels { get; protected set; }
        protected void AddPixel((int X, int Y) coord) {
            _coords.Add(coord);
            Pixels++;
            if (coord.X < MinX)
                MinX = coord.X;
            if (coord.X > MaxX)
                MaxX = coord.X;
            if (coord.Y < MinY)
                MinY = coord.Y;
            if (coord.Y > MaxY)
                MaxY = coord.Y;
        }
    }
    internal class ComponentBW_byte : ComponentBW<byte> {
        public ComponentBW_byte(Queue<(int, int)> q, int ID) : base(q, ID) {
            Finish();
        }
        public ComponentBW_byte(IList<(int, int)> list, int ID) : base(list, ID) {
            Finish();
        }
        public MatrixBW Matrix { get; protected set; }

        public override byte this[int x, int y] { get => Matrix[x, y]; protected set => Matrix[x, y] = value; }

        public virtual void Finish() {
            long sumX = 0, sumY = 0;
            Width = (MaxX - MinX + 1);
            Height = (MaxY - MinY + 1);

            var m = new MatrixBW(Width, Height);
            m.SetAllToMax();
            foreach (var c in _coords) {
                m[c.X - MinX, c.Y - MinY] = Colors.Black_byte;
                sumX += c.X;
                sumY += c.Y;
            }
            CentroidX = (sumX / Pixels) - MinX;
            CentroidY = (sumY / Pixels) - MinY;
            Matrix = m;
            _coords.Clear();
        }
    }
    internal class ComponentBW_bit : ComponentBW<byte> {
        public ComponentBW_bit(Queue<(int, int)> q, int ID) : base(q, ID) {
            Finish();
        }
        public ComponentBW_bit(IList<(int, int)> list, int ID) : base(list, ID) {
            Finish();
        }
        public MatrixBit Matrix { get; protected set; }
        public override byte this[int x, int y] { get => (Matrix[x, y]) ? Colors.Black_byte : Colors.White_byte; protected set => Matrix[x, y] = (value == Colors.Black_byte); }

        public virtual void Finish() {
            long sumX = 0, sumY = 0;
            Width = (MaxX - MinX + 1);
            Height = (MaxY - MinY + 1);

            var m = new MatrixBit(Width, Height);
            foreach (var c in _coords) {
                m[c.X - MinX, c.Y - MinY] = true;
                sumX += c.X;
                sumY += c.Y;
            }
            CentroidX = (sumX / Pixels) - MinX;
            CentroidY = (sumY / Pixels) - MinY;
            Matrix = m;
            _coords.Clear();
        }
    }
}
