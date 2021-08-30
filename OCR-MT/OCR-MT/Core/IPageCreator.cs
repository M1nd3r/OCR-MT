using System.Collections.Generic;

namespace OCR_MT.Core {
    interface IPageCreator {
        IPage<byte>[] Create(ICollection<string> paths);
    }
}
