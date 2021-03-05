using System;
using System.Collections.Generic;
using System.Text;

namespace OCR_MT.Core {
    internal abstract class ComponentBWFactory {
        internal abstract int GetID { get; }
        internal abstract (int x, int y) GetNextPixel();
        internal abstract bool HasNextPixel { get; }
    }
}
