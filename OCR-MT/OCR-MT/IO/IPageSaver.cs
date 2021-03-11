using OCR_MT.Core;

namespace OCR_MT.IO {
    interface IPageSaver<T> {
        public void Save(IPage<T> page, string path);
    }
}
