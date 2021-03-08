using System;
using System.Collections.Generic;
using System.Text;

namespace OCR_MT.Core {
    internal interface IComponent {
        public int ID { get; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int MaxX { get; }
        public int MinX { get; }
        public int MaxY { get; }
        public int MinY { get; }
        public float CentroidX { get; }
        public float CentroidY { get; }
        public long Pixels { get; }
    }
    internal interface IComponent<T> : IComponent {
        public T this[int x, int y] { get; }
    }
}
