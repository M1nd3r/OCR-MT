using System;
using System.Collections.Generic;

namespace OCR_MT.Core {
    class DocumentCreator_MT : IDocumentCreator,IDisposable
        {
        protected IPageCreator creator;
        private ICollection<string> paths;
        private IDocument<byte> doc;
        public DocumentCreator_MT() {
            SetCreator();
        }
        public IDocument<byte> Create(ICollection<string> paths) {
            this.paths = paths;
            return CreateDocumentWithPages();
        }
        private void SetCreator() {
            this.creator = new PageCreator_MT();
        }
        private IDocument<byte> CreateDocumentWithPages() {
            CreateNewDocument();
            AddPagesToDocument();
            var doc = this.doc;
            return doc;
        }
        public void Dispose() {
            ClearCreator();
            ClearDoc();
            ClearPaths();
        }
        private void CreateNewDocument() {
            doc = Document.GetDocument();
        }
        private void AddPagesToDocument() {
            var pages = creator.Create(paths);
            foreach (var page in pages) {
                doc.AddPage(page);
            }
        }        
        private void ClearDoc() => this.doc = null;
        private void ClearCreator() => this.creator = null;
        private void ClearPaths() => this.paths = null;
    }
}
