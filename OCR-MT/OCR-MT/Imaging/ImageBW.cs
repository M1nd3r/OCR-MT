using System;
using System.Collections.Generic;
using System.Text;

namespace OCR_MT.Imaging {
    class ImageBW {
        private int sizeX, sizeY;
        private byte[,] matrix;
        public int SizeX { get; }
        public int SizeY { get; }
        public byte[,] Matrix { get => Matrix; }

    }
}
