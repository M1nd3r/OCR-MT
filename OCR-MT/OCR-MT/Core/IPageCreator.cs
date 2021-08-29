using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Core {
    interface IPageCreator {
        IPage<byte>[] Create(ICollection<string> paths);
    }
}
