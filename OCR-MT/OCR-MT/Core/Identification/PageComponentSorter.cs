using System;
using System.Collections.Generic;

namespace OCR_MT.Core.Identification {
    class PageComponentSorter : ISorterPageSetable {
        protected readonly IAlphabet _alphabet;
        protected readonly IAlphabet _alphabetMutated;
        protected IPage<byte> _page;
        protected readonly IComponentSortHandler _handler;
        protected readonly int _alphabetCount;
        private List<IList<LetterComponentDist>> _sortedLetters;
        public PageComponentSorter(in IAlphabet alphabet, in IPage<byte> page,
            IComponentSortHandler handler
            ) {
            if (alphabet == null)
                throw new ArgumentNullException("Argument " + nameof(IAlphabet) + " " + nameof(alphabet) + " is null");

            if (handler == null)
                throw new ArgumentNullException("Argument " + nameof(IComponentSortHandler) + " " + nameof(handler) + " is null");
            _alphabet = alphabet;
            _alphabetCount = alphabet.GetLetters.Count;
            _page = page;
            _handler = handler;
            _alphabetMutated = _handler.Mutate(_alphabet);
        }

        public IAlphabet GetAlphabet => _alphabet;

        public IPage<byte> GetPage => _page;
        public void SetPage(in IPage<byte> page) => _page = page; //TODO not threadsafe

        public void Sort(out IList<IList<LetterComponentDist>> sortedLetters) {
            if (_page == null)
                throw new ArgumentNullException("Variable " + nameof(IPage<byte>) + " " + nameof(_page) + " is null. There is no page to be acted on!");

            _sortedLetters = InitializeSortedLettersList();
            for (int i = 0; i < _page.Components.Count; i++) {
                var component = _handler.Mutate(_page.Components[i]);
                SortSingleComponent(component);
            }
            sortedLetters = _sortedLetters;
        }
        private List<IList<LetterComponentDist>> InitializeSortedLettersList() {
            var sortedLetters = new List<IList<LetterComponentDist>>(_alphabetCount);
            for (int i = 0; i < _alphabetCount; i++) {
                sortedLetters.Add(new List<LetterComponentDist>());
            }
            return sortedLetters;
        }
        private void SortSingleComponent(IComponent<byte> component) {
            (int index, double distance) = GetIndexAndMinDistance(component);
            _sortedLetters[index].Add(new(_alphabet[index], component, distance));
        }
        private (int, double) GetIndexAndMinDistance(IComponent<byte> component) {
            int indexMin = -1;
            double minDist = Int32.MaxValue;
            for (int y = 0; y < _alphabetCount; y++) {
                var distance = _handler.Distance(_alphabet[y], component);
                if (distance < minDist) {
                    minDist = distance;
                    indexMin = y;
                }
            }
            return (indexMin, minDist);
        }
    }
}
