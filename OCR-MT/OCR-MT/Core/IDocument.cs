using System.Collections.Generic;

namespace OCR_MT.Core{
    internal interface IDocument {
        public int ID { get; }   
    }
    internal interface IDocument<T> : IDocument {
        void AddPage(IPage<T> page);
        public IList<IPage<T>> GetPages { get; }
    }
}
