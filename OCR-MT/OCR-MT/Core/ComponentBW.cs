using System;
using System.Collections.Generic;
using static OCR_MT.Utils.Constants;
using static OCR_MT.Utils.Extensions;

namespace OCR_MT.Core {
    internal abstract class ComponentBW<T> : IComponent<T> {
        protected List<(int X, int Y)> coords = new List<(int X, int Y)>();
        public ComponentBW(int ID) {
            this.ID = ID;
        }
        public ComponentBW(Queue<(int, int)> q, int ID) {
            this.ID = ID;
            while (q.Count > 0)
                AddPixel(q.Dequeue());
        }

        public abstract T this[int x, int y] { get; protected set; }

        public int ID { get; }

        public int SizeX { get; protected set; }

        public int SizeY { get; protected set; }

        public int MaxX { get; protected set; }
        public int MinX { get; protected set; }
        public int MaxY { get; protected set; }
        public int MinY { get; protected set; }
        public float CentroidX { get; protected set; }
        public float CentroidY { get; protected set; }
        public long Pixels { get; protected set; }
        protected void AddPixel((int X, int Y) coord) {
            coords.Add(coord);
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
        public MatrixBW Matrix { get; protected set; }

        public override byte this[int x, int y] { get => Matrix[x, y]; protected set => Matrix[x, y] = value; }

        public virtual void Finish() {
            long sumX = 0, sumY = 0;
            SizeX = (MaxX - MinX + 1);
            SizeY = (MaxY - MinY + 1);

            var m = new MatrixBW(SizeX, SizeY);
            m.SetAllToZero();
            foreach (var c in coords) {
                m[c.X - MinX, c.Y - MinY] = 1;
                sumX += c.X;
                sumY += c.Y;
            }
            CentroidX = (sumX / Pixels) - MinX;
            CentroidY = (sumY / Pixels) - MinY;
            Matrix = m;
            coords.Clear();
        }
    }
    internal class ComponentBW_bit : ComponentBW<byte> {
        public ComponentBW_bit(Queue<(int, int)> q, int ID) : base(q, ID) {
            Finish();
        }
        public MatrixBit Matrix { get; protected set; }
        public override byte this[int x, int y] { get => (Matrix[x, y]) ? Colors.Black_byte : Colors.White_byte; protected set => Matrix[x, y] = (value == Colors.Black_byte); }

        public virtual void Finish() {
            long sumX = 0, sumY = 0;
            SizeX = (MaxX - MinX + 1);
            SizeY = (MaxY - MinY + 1);

            var m = new MatrixBit(SizeX, SizeY);
            foreach (var c in coords) {
                m[c.X - MinX, c.Y - MinY] = true;
                sumX += c.X;
                sumY += c.Y;
            }
            CentroidX = (sumX / Pixels) - MinX;
            CentroidY = (sumY / Pixels) - MinY;
            Matrix = m;
            coords.Clear();
        }
    }
}
