using System;
using System.Collections.Generic;
using System.Text;
using OCR_MT.Core;

namespace OCR_MT.Imaging {
    interface IImage<T> {
        public int Width { get; }
        public int Height { get; }
        public T this[int x, int y] { get; }           
        
    }
    interface BW {
        public byte Intensity { get; }
    }
}
