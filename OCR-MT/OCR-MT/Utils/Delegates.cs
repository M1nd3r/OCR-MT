using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Utils {
    internal static class Delegates {
        public delegate double DelegateMetric<T>(T A, T B);
        public delegate bool DelegateTreshold<T>(T value);
        public delegate IList<T> DelegateFilter<T>(IList<T> list);

    }
}
