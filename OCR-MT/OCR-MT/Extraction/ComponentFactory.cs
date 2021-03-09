using OCR_MT.Core;
using System.Collections.Generic;

namespace OCR_MT.Extraction {
    abstract class ComponentFactory<T> {
        public abstract IComponent<T> CreateComponent(Queue<(int, int)> q);
    }
    class ComponentFactoryBlack : ComponentFactory<byte> {
        private static int _counterID = 5;
        public override IComponent<byte> CreateComponent(Queue<(int, int)> q) => new ComponentBW_byte(q, _counterID++);           
        
    }
    class ComponentFactoryWhite : ComponentFactory<byte> {
        private static int _counterID = -5;
        public override IComponent<byte> CreateComponent(Queue<(int, int)> q) => new ComponentBW_byte(q, _counterID--);
    }
}
