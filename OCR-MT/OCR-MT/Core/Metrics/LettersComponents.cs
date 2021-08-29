
using OCR_MT.Core.Identification;
using System;

namespace OCR_MT.Core.Metrics {
    internal static class LettersComponents {
        public static double SizeMetric(ILetter letter, IComponent<byte> component) {
            return 
                Math.Sqrt(
                    (letter.Height - component.Height) * (letter.Height - component.Height)
                    + (letter.Width - component.Width) * (letter.Width - component.Width)
                );
        }
    }
}
