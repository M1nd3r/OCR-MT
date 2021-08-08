using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OCR_MT.Utils.Delegates;

namespace OCR_MT.Core.Identification {
    class PageComponentSorter : ISorterWithAlphabet {
        private readonly DelegateMetric<IComponent<byte>> _dist;
        private readonly IAlphabet _alphabet;
        private readonly IPage<byte> _page;
        public PageComponentSorter(IAlphabet alphabet, IPage<byte> page, DelegateMetric<IComponent<byte>> dist) {
            if (alphabet == null)
                throw new ArgumentNullException("Argument " + nameof(IAlphabet) + " " + nameof(alphabet) + " is null");
            if (page == null)
                throw new ArgumentNullException("Argument " + nameof(IPage) + " " + nameof(page) + " is null");
            if (dist == null)
                throw new ArgumentNullException("Argument " + nameof(DelegateMetric<IComponent<byte>>) + " " + nameof(dist) + " is null");

            _alphabet = alphabet;
            _page = page;
            _dist = dist;
        }

        public IAlphabet GetAlphabet => _alphabet;

        public IPage<byte> GetPage => _page;

        public void Sort(out IList<IList<LetterComponentDist>> sortedLetters) {
            var alphabetCount = _alphabet.GetLetters.Count;
            sortedLetters = new List<IList<LetterComponentDist>>(alphabetCount);
            for (int i = 0; i < alphabetCount; i++) {
                sortedLetters.Add(new List<LetterComponentDist>());
            }
            foreach (var component in _page.Components) {
                int indexMin = -1;
                double minDist = Int32.MaxValue;
                for (int i = 0; i < alphabetCount; i++) {
                    var distance = _dist(_alphabet[i], component);
                    if (distance < minDist) {
                        minDist = distance;
                        indexMin = i;
                    }
                }
                sortedLetters[indexMin].Add(new(_alphabet[indexMin], component, minDist));
            }
        }
    }
}
