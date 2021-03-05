using System;
using System.Collections.Generic;
using System.Text;

namespace OCR_MT.Core{
    internal interface IDocument {
        public int ID { get; }
        void AddPage();
    }
}
