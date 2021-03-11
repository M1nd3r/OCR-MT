using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Core {
    class Page : IPage<byte> {
        public Page(int ID, IList<IComponent<byte>> components, int width, int height) {
            this.ID = ID;
            this.Components = components;
            this.Width = width;
            this.Height = height;
        }

        public int ID {get;}

        public int Width { get; }

        public int Height { get; }

        public IList<IComponent<byte>> Components { get; }
    }
}
