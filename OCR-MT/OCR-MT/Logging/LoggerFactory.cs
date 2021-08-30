namespace OCR_MT.Logging {
    internal static class LoggerFactory {
        public static ILogger GetLogger() => new LoggerConsoleMT();
    }
}
