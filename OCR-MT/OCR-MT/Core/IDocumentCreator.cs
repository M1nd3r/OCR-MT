using System.Collections.Generic;

namespace OCR_MT.Core {
    interface IDocumentCreator {
        IDocument<byte> Create(ICollection<string> paths);
    }
}
