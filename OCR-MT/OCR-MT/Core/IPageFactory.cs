using OCR_MT.Imaging;

namespace OCR_MT.Core {
    interface IPageFactory<T,U> {
        public IPage<U> Create(IImage<T> img);
    }
}
