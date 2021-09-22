using System.Collections.Generic;

namespace OCR_MT.Core {
    class Document : IDocument<byte> {
        private static int counterID = 0;
        public static Document GetDocument() {
            return new Document(++counterID);
        }

        protected IList<IPage<byte>> pages;
        private readonly int id;
        private Document(int ID) {
            this.id = ID;
            this.pages = new List<IPage<byte>>();
        }        
        public IList<IPage<byte>> GetPages => pages;
        public int ID => id;
        public void AddPage(IPage<byte> page) {
            this.pages.Add(page);
        }
    }
}
