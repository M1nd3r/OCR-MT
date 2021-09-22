using System;
using static OCR_MT.Utils.Delegates;

namespace OCR_MT.Core.Identification {

    abstract class APageComposerBase<T> : IPageComposer where T:ILetter {
        protected ISorter<T> sorter=null;
        protected IAlphabet alphabet=null;
        abstract public IPage<byte> Compose(IPage<byte> page);
        abstract public IDocument<byte> Compose(IDocument<byte> document);
        public APageComposerBase<T> SetSorter(ISorter<T> sorter) {
            this.sorter = sorter;
            return this;
        }
        public APageComposerBase<T> SetAlphabet(IAlphabet alphabet) {
            this.alphabet = alphabet;
            return this;
        }
        public virtual void Dispose() {
            this.sorter = null;
            this.alphabet = null;
        }
        protected void VerifyComposerIsCorrectlySet() {
            VerifySorterIsSet();
            VerifyAlphabetIsSet();
        }
        private void VerifySorterIsSet() {
            if (sorter == null)
                throw new SorterIsNotCorrectlySetException();
        }
        private void VerifyAlphabetIsSet() {
            if (alphabet == null)
                throw new AlphabetIsNotCorrectlySetException();
        }
        private class ComposerIsNotCorrectlySetException : Exception { }
        private class SorterIsNotCorrectlySetException : ComposerIsNotCorrectlySetException { }
        private class AlphabetIsNotCorrectlySetException : ComposerIsNotCorrectlySetException { }
    }
    sealed class PageComposer<T> : APageComposerBase<T> where T:ILetter {
        private IDocument<byte> originalDoc = null;
        private DelegateFilter<T> filter = null;
        public override IPage<byte> Compose(IPage<byte> page) {
            VerifyArgumentIsNotNull(page);
            VerifyComposerIsCorrectlySet();

            var newPage = ComposePage(page);
            Clean();
            return newPage;
        }
        public override IDocument<byte> Compose(IDocument<byte> document) {
            VerifyArgumentIsNotNull(document);
            VerifyComposerIsCorrectlySet();

            var doc = ComposeDocument(document);
            Clean();
            return doc;
        }
        public void SetFilter(DelegateFilter<T> filter) {
            this.filter = filter;
        }
        private IPage<byte> ComposePage(IPage<byte> page) {
            var sortedLetters = sorter.Sort(page);
            return new PageFactoryLetters<T>(page, sortedLetters, filter ?? (i => i)).Create();
        }
        private IDocument<byte> ComposeDocument(IDocument<byte> document) {
            this.originalDoc = document;
            var doc = CreateNewDocument();
            AddComposedPagesToDocument(doc);
            return doc;
        }
        private IDocument<byte> CreateNewDocument() {
            return Document.GetDocument();
        }
        private void AddComposedPagesToDocument(IDocument<byte> document) {
            foreach (var page in originalDoc.GetPages) {
                document.AddPage(Compose(page));
            }
        }
        private void VerifyArgumentIsNotNull(object arg) {
            if(arg==null)
                throw new ArgumentNullException();
        }
        private void Clean() {
            this.originalDoc = null;
        }
    }
}
