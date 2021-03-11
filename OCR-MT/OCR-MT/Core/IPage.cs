using OCR_MT.Core;
using System;
using System.Collections.Generic;
using System.Text;

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
