using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Core.Identification {
    interface ISorter {
        public void Sort(out IList<IList<LetterComponentDist>> sortedLetters);
        public IPage<byte> GetPage { get; }
    }
    interface ISorterWithAlphabet : ISorter {
        public IAlphabet GetAlphabet { get; }
    }
}
