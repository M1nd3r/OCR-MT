using System;
using System.Collections.Generic;
using static OCR_MT.Utils.Extensions;

namespace OCR_MT.Core {
    internal abstract class ComponentBW : IComponent {
        protected List<(int X, int Y)> coords = new List<(int X, int Y)>();
        public ComponentBW(int ID) {
            this.ID = ID;
        }
        public int ID { get;  }

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
    internal class ComponentBW_byte : ComponentBW {        
        public static ComponentBW_byte Create(ComponentBWFactory factory) {
            ComponentBW_byte r = new ComponentBW_byte(factory.GetID);
            while (factory.HasNextPixel) {
                r.AddPixel(factory.GetNextPixel());
            }
            r.Finish();
            return r;
        }
        private ComponentBW_byte(int ID) : base(ID) { }
        public Matrix<byte> Matrix { get; protected set; }
        public virtual void Finish() {
            long sumX = 0, sumY = 0;
            SizeX = (MaxX - MinX + 1);
            SizeY = (MaxY - MinY + 1);

            var m = new Matrix<byte>(SizeX, SizeY);
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
    internal class ComponentBW_bit:ComponentBW {
        public static ComponentBW_bit Create(ComponentBWFactory factory) {
            ComponentBW_bit r = new ComponentBW_bit(factory.GetID);
            while (factory.HasNextPixel) {
                r.AddPixel(factory.GetNextPixel());
            }
            r.Finish();
            return r;
        }
        private ComponentBW_bit(int ID) : base(ID) { }
        public MatrixBit Matrix { get; protected set; }

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
