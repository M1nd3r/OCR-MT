using OCR_MT.Core;
using OCR_MT.IO;
using OCR_MT.Logging;
using OCR_MT.Utils;

namespace OCR_MT.IO {
    class PageSaverBasic:IPageSaver<byte> {
        private static ILogger logger = LoggerFactory.GetLogger();
        public void Save(IPage<byte> page, string path) {
            MatrixBW m = new MatrixBW(page.Width, page.Height);
            m.SetAllToMax();
            foreach (var component in page.Components) {
                for (int y = 0; y < component.Height; y++) {
                    for (int x = 0; x < component.Width; x++) {
                        m[x + component.MinX, y + component.MinY] = component[x, y];
                    }
                }
            }
            ImageBWSaver.Save(m, path);
            logger.Out(nameof(PageSaverBasic) + "." + nameof(Save) + ": Saved page (" + page.ID + ")");
        }
    }
}
