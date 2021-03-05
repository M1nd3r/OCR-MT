using System;
using System.Collections.Generic;
using System.Text;

namespace OCR_MT.Core {
    internal class ComponentBWFactory {
        internal static ComponentBWCreator GetComponentBWCreator() {
            return new ComponentBWCreatorDefault();
        }

    }
}
