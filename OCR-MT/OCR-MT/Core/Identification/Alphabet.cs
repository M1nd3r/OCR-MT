using System.Collections.Generic;

namespace OCR_MT.Core.Identification {
    class Alphabet:IAlphabet {
        public IList<ILetter> Letters { get; protected set; } 
        public Alphabet(IList<ILetter> letters) {
            this.Letters = letters;
        }
        public IList<ILetter> GetLetters => this.Letters;      
        public ILetter this[int i] { get => this.Letters[i]; }
    }
}
