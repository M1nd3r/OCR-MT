using System;
using OCR_MT.MT;
using OCR_MT.Imaging;
using System.Collections.Generic;
using System.Threading;
using OCR_MT.Core;
using OCR_MT.Logging;
using SixLabors.ImageSharp.PixelFormats;

namespace OCR_MT.Imaging {
    class MatrixBWParser_MT : IMatrixBWParser<byte>, IMultiThread, IDisposable {
        private ILogger logger = LoggerFactory.GetLogger();
        private int _index;
        public MatrixBWParser_MT() {
            _index = 0;
        }
        public int NumberOfThreads => _index;

        public void InitializeThread() {            
                _index++;            
        }

        public MatrixBW Parse(IImage<byte> image) {
            logger.Out(nameof(MatrixBWParser_MT) + "." + nameof(Parse) + ": Start");
            var m = new MatrixBW(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++) {
                for (int y = 0; y < image.Height; y++) {
                    m[x, y] = image[x, y];
                }
            }
            return m;
        }
        public void Dispose() {
            logger.Out(nameof(MatrixBWParser_MT) + nameof(Dispose) + ":  Disposing");
        }
    }
}
