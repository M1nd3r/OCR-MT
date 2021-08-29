using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OCR_MT.Utils.Delegates;

namespace OCR_MT.Core.Identification {
    class PageComponentSorter : ISorterPageSetable {
        protected readonly IAlphabet _alphabet;
        protected IAlphabet _alphabetMutated;
        protected IPage<byte> _page;
        protected readonly IComponentSortHandler _handler;
        public PageComponentSorter(in IAlphabet alphabet, in IPage<byte> page, 
            IComponentSortHandler handler
            ) 
        {
            if (alphabet == null)
                throw new ArgumentNullException("Argument " + nameof(IAlphabet) + " " + nameof(alphabet) + " is null");

            if (handler == null)
                throw new ArgumentNullException("Argument " + nameof(IComponentSortHandler) + " " + nameof(handler) + " is null");
            _alphabet = alphabet;
            _page = page;
            _handler = handler;
            Initialize();
        }

        public IAlphabet GetAlphabet => _alphabet;

        public IPage<byte> GetPage => _page;
        public void SetPage(in IPage<byte> page) => _page = page; //TODO not threadsafe

        private void Initialize()
        {
            _alphabetMutated = _handler.Mutate(_alphabet);
        }

        public void Sort(out IList<IList<LetterComponentDist>> sortedLetters) {
            if (_page == null)
                throw new ArgumentNullException("Variable " + nameof(IPage<byte>) + " " + nameof(_page) + " is null. There is no page to be acted on!");
            var alphabetCount = _alphabetMutated.GetLetters.Count;
            sortedLetters = new List<IList<LetterComponentDist>>(alphabetCount);
            for (int i = 0; i < alphabetCount; i++) {
                sortedLetters.Add(new List<LetterComponentDist>());
            }
            for (int i = 0; i < _page.Components.Count; i++) {
                var component = _handler.Mutate(_page.Components[i]);
                int indexMin = -1;
                double minDist = Int32.MaxValue;
                for (int y = 0; y < alphabetCount; y++) {
                    var distance = _handler.Distance(_alphabet[y], component);
                    if (distance < minDist) {
                        minDist = distance;
                        indexMin = y;
                    }
                }
                sortedLetters[indexMin].Add(new(_alphabet[indexMin], component, minDist));
            }
        }
    }
}
