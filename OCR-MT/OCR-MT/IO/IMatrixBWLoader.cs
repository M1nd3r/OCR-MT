using System;
using System.Collections.Generic;
using System.Text;
using OCR_MT.Core;

namespace OCR_MT.IO {
    interface IMatrixBWLoader {
        public MatrixBW Load(string path);
        public IEnumerable<MatrixBW> Load(ICollection<string> path);

    }
}
