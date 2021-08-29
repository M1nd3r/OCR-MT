using OCR_MT.Core;
using OCR_MT.Core.Detection;
using OCR_MT.IO;
using static OCR_MT.Utils.Constants.Suffixes;

namespace OCR_MT.Experiments {
    internal static class CreateAnalyseSave {
        private static IPage<byte>[] pages = null;
        private static string
                sourcePath = @"D:\GitHub\OCR-CS\OCR\OCR\images\Karpaty\", //pages to analyse
                alphabetPath = @"D:\GitHub\OCR-MT\abeceda\abeceda_Verne\", //alphabet to be used - path of pseduo-root folder of alphabet
                outputFolder = @"D:\GitHub\OCR-MT\abeceda\2\"; //folder where to output translated pages
        public static void Run() {            
            Analyser a = new Analyser(sourcePath, alphabetPath);
            pages = a.Analyse();            
            SavePages();
        }
        private static void SavePages() {
            var saver = GetPageSaver();
            foreach (var page in pages) {
                saver.Save(page, outputFolder + page.ID + Png);
            }
        }
        private static IPageSaver<byte> GetPageSaver() {
            return new PageSaverTresholded(i => {
                if (i < 10) return true;
                return false;
            });
        }
        
    }
}
