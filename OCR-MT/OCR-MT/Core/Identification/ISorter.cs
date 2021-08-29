using System.Collections.Generic;

namespace OCR_MT.Core.Identification {
    interface ISorter {
        public void Sort(out IList<IList<LetterComponentDist>> sortedLetters);
        public IPage<byte> GetPage { get; }
    }
    interface ISorterWithAlphabet : ISorter {
        public IAlphabet GetAlphabet { get; }
    }
    interface ISorterPageSetable : ISorterWithAlphabet {
        public void SetPage(in IPage<byte> page);
    }
}
