using OCR_MT.Core;
using OCR_MT.Core.Identification;
using OCR_MT.Utils;
using static OCR_MT.Utils.Delegates;
using static OCR_MT.Utils.Constants.Colors;

namespace OCR_MT.IO {
    class PageSaverTresholded : PageSaverBasic {
        protected DelegateTreshold<double> _treshold;
        public PageSaverTresholded(DelegateTreshold<double> treshold) {
            _treshold = treshold;
        }
        public override void Save(IPage<byte> page, string path) {
            MatrixBWR m = new MatrixBWR(page.Width, page.Height);
            m.SetAllToMax();
            foreach (LetterComponentDist component in page.Components) {
                byte foregroundColor = Black_byte;
                IComponent<byte> cp = component;

                if (!_treshold(component.distance)) {
                    foregroundColor = Red_byte;
                    cp = component.component;
                }
                for (int y = 0; y < cp.Height; y++) {
                    for (int x = 0; x < cp.Width; x++) {
                        m[x + cp.MinX, y + cp.MinY] = (cp[x, y] == White_byte) ? White_byte : foregroundColor;
                    }
                }
            }
            ImageBWRSaver.Save(m, path);
            logger.Out(nameof(PageSaverTresholded) + "." + nameof(Save) + ": Saved page (" + page.ID + ")");
        }
    }
}
