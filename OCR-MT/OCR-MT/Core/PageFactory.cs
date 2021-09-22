using OCR_MT.Core.Identification;
using OCR_MT.Extraction;
using OCR_MT.Imaging;
using System;
using System.Collections.Generic;
using static OCR_MT.Utils.Constants;
using static OCR_MT.Utils.Delegates;

namespace OCR_MT.Core {
    class PageFactory : IPageFactory<byte> {
        protected static int _pageCounter = 1;
        protected readonly IImage<byte> _img;
        public PageFactory(IImage<byte> img) {
            if (img is null) {
                throw new ArgumentNullException(nameof(_img));
            }
            _img = img;
        }
        public static int GetCounter { get => _pageCounter; }
        public IPage<byte> Create() {

            IList<IComponent<byte>> components = new List<IComponent<byte>>();
            ComponentBWExtractor ce = new ComponentBWExtractor();
            components = ce.Extract(_img, Colors.Black_byte, (_pageCounter + 1).ToString());
            return new Page(_pageCounter++, components, _img.Width, _img.Height);
        }
    }
    class PageFactory_MT : PageFactory {
        public PageFactory_MT(IImage<byte> img) : base(img) { }
        public IPage<byte> Create(int ID) {
            _pageCounter++;
            IList<IComponent<byte>> components = new List<IComponent<byte>>();
            ComponentBWExtractor ce = new ComponentBWExtractor();
            components = ce.Extract(_img, Colors.Black_byte, ID.ToString());
            return new Page(ID, components, _img.Width, _img.Height);
        }
    }
    class PageFactoryLetters<T> : IPageFactory<byte> where T:ILetter {
        private readonly IPage _page;
        private readonly IList<IList<T>> _arrangedComponents;
        private readonly DelegateFilter<T> _filter;
        public PageFactoryLetters(IPage page, IList<IList<T>> arrangedComponents, DelegateFilter<T> filter) {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            if (arrangedComponents == null)
                throw new ArgumentNullException(nameof(arrangedComponents));
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            _page = page;
            _arrangedComponents = arrangedComponents;
            _filter = filter;
        }
        public IPage<byte> Create() {
            List<IComponent<byte>> filteredComponentsList = new List<IComponent<byte>>();
            for (int i = 0; i < _arrangedComponents.Count; i++) {
                var x = _filter(_arrangedComponents[i]);
                foreach (var item in x) {
                    filteredComponentsList.Add(item);
                }
            }

            int widthAdd = 0, heightAdd = 0;
            for (int i = 0; i < filteredComponentsList.Count; i++) {
                var a = filteredComponentsList[i];
                var widthDiff = a.Width + a.MinX - _page.Width;
                var heightDiff = a.Height + a.MinY - _page.Height;

                if (widthDiff > widthAdd)
                    widthAdd = widthDiff;
                if (heightDiff > heightAdd)
                    heightAdd = heightDiff;
            }
            return new Page(_page.ID, filteredComponentsList, _page.Width + widthAdd, _page.Height + heightAdd);
        }
    }
}
