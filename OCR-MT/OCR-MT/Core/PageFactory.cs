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
    class PageFactoryLetters : IPageFactory<byte> {
        private readonly IPage _page;
        private readonly IList<IList<LetterComponentDist>> _arrangedComponents;
        private readonly DelegateFilter<LetterComponentDist> _filter;
        public PageFactoryLetters(IPage page, IList<IList<LetterComponentDist>> arrangedComponents, DelegateFilter<LetterComponentDist> filter) {
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
                filteredComponentsList.AddRange(_filter(_arrangedComponents[i]));
            }

            int widthAdd = 0, heightAdd = 0;
            for (int i = 0; i < filteredComponentsList.Count; i++) {
                var a = (filteredComponentsList[i] as LetterComponentDist);
                var widthDiff = _page.Width - a.Width - a.component.MinX;
                var heightDiff = _page.Height - a.Height - a.component.MinY;

                if (widthDiff > widthAdd)
                    widthAdd = widthDiff;
                if (heightDiff > heightAdd)
                    heightAdd = heightDiff;
            }
            return new Page(_page.ID, filteredComponentsList, _page.Width + widthAdd, _page.Height + heightAdd);
        }
    }
}
