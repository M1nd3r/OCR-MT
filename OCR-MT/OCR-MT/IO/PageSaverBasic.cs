using OCR_MT.Core;
using OCR_MT.Logging;
using OCR_MT.Utils;

namespace OCR_MT.IO {
    class PageSaverBasic:IPageSaver<byte> {
        protected static ILogger logger = LoggerFactory.GetLogger();
        private MatrixBW _matrix;
        public virtual void Save(IPage<byte> page, string path) {
            InitializeMatrix(page);
            foreach (var component in page.Components) {
                DrawComponentIntoMatrix(component);
            }
            ImageBWSaver.Save(_matrix, path);
            CleanAfterSave();
            logger.Out(nameof(PageSaverBasic) + "." + nameof(Save) + ": Saved page (" + page.ID + ")");

        }
        private void DrawComponentIntoMatrix(IComponent<byte> component) {
            for (int y = 0; y < component.Height; y++) {
                for (int x = 0; x < component.Width; x++) {
                    _matrix[x + component.MinX, y + component.MinY] = component[x, y];
                }
            }
        }
        private void InitializeMatrix(IPage<byte> page) {
            _matrix = new MatrixBW(page.Width, page.Height);
            _matrix.SetAllToMax();
        }
        private void CleanAfterSave() {
            SetMatrixToNull();
        }
        private void SetMatrixToNull() {
            _matrix = null;
        }
    }
}
