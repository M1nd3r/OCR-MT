using OCR_MT.Core.Identification;
using System;

namespace OCR_MT.Core.Metrics {
    internal static class CleverComponents {
        public static double SizeMetric(ICleverComponent a, ICleverComponent b) {
            return
                Math.Sqrt(
                    (a.GetComponent.Height - b.GetComponent.Height) 
                    * (a.GetComponent.Height - b.GetComponent.Height)
                    + (a.GetComponent.Width - b.GetComponent.Width) 
                    * (a.GetComponent.Width - b.GetComponent.Width)
                );
        }
    }
}