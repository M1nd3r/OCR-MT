using System;

namespace OCR_MT.Core.Identification {
    interface IPageComposer:IDisposable{
        public IPage<byte> Compose(IPage<byte> page);
        public IDocument<byte> Compose(IDocument<byte> document);
    }
}
