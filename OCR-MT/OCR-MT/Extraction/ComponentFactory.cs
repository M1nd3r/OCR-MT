using OCR_MT.Core;
using System.Collections.Generic;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.Extraction {
    abstract class ComponentFactory<T> {
        public abstract IComponent<T> CreateComponent(Queue<(int, int)> q);
    }
    class ComponentFactory {
        public static ComponentFactory<byte> Get(byte target) {
            if (target == Colors.Black_byte)
                return new ComponentFactoryBlack();
            if (target == Colors.White_byte)
                return new ComponentFactoryWhite();
            return new ComponentFactoryByte();
        }
    }
    class ComponentFactoryBlack : ComponentFactory<byte> {
        private static int _counterID = 5;
        public override IComponent<byte> CreateComponent(Queue<(int, int)> q) => new ComponentBW_byte(q, _counterID++);           
        
    }
    class ComponentFactoryWhite : ComponentFactory<byte> {
        private static int _counterID = -5;
        public override IComponent<byte> CreateComponent(Queue<(int, int)> q) => new ComponentBW_byte(q, _counterID--);
    }
    class ComponentFactoryByte: ComponentFactory<byte> {
        private static int _counterID = int.MaxValue-1;
        public override IComponent<byte> CreateComponent(Queue<(int, int)> q) => new ComponentBW_byte(q, _counterID--);
    }
}
