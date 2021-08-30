using System.Collections.Generic;

namespace OCR_MT.Core {
    internal interface IPage {
        public int ID { get; }
        public int Width { get; }
        public int Height { get; }
    }
    internal interface IPage<T> : IPage {
        public IList<IComponent<T>> Components { get; }
    }
}
