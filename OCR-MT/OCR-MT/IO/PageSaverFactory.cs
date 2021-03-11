namespace OCR_MT.IO {
    class PageSaverFactory {
        private static IPageSaver<byte> _pageSaver=null;
        public static IPageSaver<byte> Get() {
            if (_pageSaver == null)
                _pageSaver = new PageSaverBasic();
            return _pageSaver;
        }
    }
}
