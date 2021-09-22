using System.Collections.Generic;

namespace OCR_MT.Core.Identification {
    interface ISorter<T> where T : ILetter {
        public IList<IList<T>> Sort(IPage<byte> page);
    }
    interface ICleverSorter:ISorter<ILetter> {

    }
}
