using OCR_MT.Extraction;
using OCR_MT.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.Core {
    class PageFactory : IPageFactory<byte,byte> {
        protected static int _pageCounter = 1;
        public static int GetCounter { get => _pageCounter; }
        public IPage<byte> Create(IImage<byte> img) {
            if (img is null) {
                throw new ArgumentNullException(nameof(img));
            }
            IList<IComponent<byte>> components = new List<IComponent<byte>>();
            ComponentBWExtractorFast ce = new ComponentBWExtractorFast();
            components = ce.Extract(img, Colors.Black_byte,(_pageCounter+1).ToString());
            return new Page(_pageCounter++, components, img.Width, img.Height);            
        }
    }
    class PageFactory_MT : PageFactory {
        public IPage<byte> Create(IImage<byte> img,int ID) {
            _pageCounter++;
            if (img is null) {
                throw new ArgumentNullException(nameof(img));
            }
            IList<IComponent<byte>> components = new List<IComponent<byte>>();
            ComponentBWExtractorFast ce = new ComponentBWExtractorFast();
            components = ce.Extract(img, Colors.Black_byte,ID.ToString());
            return new Page(ID, components, img.Width, img.Height);
        }
    }
}
