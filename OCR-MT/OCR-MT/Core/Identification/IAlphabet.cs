using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Core.Identification {
    interface IAlphabet {
        IList<ILetter> GetLetters { get; }
        ILetter this[int i] { get; }
    }
}
