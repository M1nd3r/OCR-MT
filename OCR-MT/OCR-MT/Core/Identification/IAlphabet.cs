using System.Collections.Generic;

namespace OCR_MT.Core.Identification {
    interface IAlphabet {
        IList<ILetter> GetLetters { get; }
        ILetter this[int i] { get; }
    }
}
