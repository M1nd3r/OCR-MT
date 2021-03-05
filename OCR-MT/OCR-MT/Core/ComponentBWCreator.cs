using System;
using System.Collections.Generic;
using System.Text;

namespace OCR_MT.Core {
    internal abstract class ComponentBWCreator {
        internal abstract int GetID { get; }
        internal abstract(int x, int y) GetNextPixel();
        internal abstract bool HasNextPixel { get; }

    }

    //TODO
    internal class ComponentBWCreatorDefault: ComponentBWCreator {
        internal override  int GetID{ get; }
        internal override (int x, int y) GetNextPixel() => throw new NotImplementedException();
        internal override bool HasNextPixel { get; }
    }
}
