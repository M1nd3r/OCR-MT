using System;
using System.Collections.Generic;
using System.Text;

namespace OCR_MT.Core {
    internal class ComponentBWFactory {
        internal static IComponent<byte> GetComponentBW() {
            return ComponentBW_byte.Create();
        }
        internal static ComponentBWCreator GetComponentBWCreator() {
            return new ComponentBWCreatorDefault();
        }

    }
}
