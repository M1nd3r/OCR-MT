using System.Collections.Generic;

namespace OCR_MT.Utils {
    internal static class Delegates {
        public delegate double DelegateMetric<T>(T A, T B);
        public delegate double DelegateMetric<T, U>(T A, U B);
        public delegate bool DelegateTreshold<T>(T value);
        public delegate IList<T> DelegateFilter<T>(IList<T> list);
    }
}
