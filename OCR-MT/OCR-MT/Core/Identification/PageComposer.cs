using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OCR_MT.Utils.Delegates;

namespace OCR_MT.Core.Identification {
    abstract class PageComposerBase : IComposer {
        protected readonly ISorter _sorter;
        protected readonly IAlphabet _alphabet;
        protected IPage<byte> _page;
        public PageComposerBase(ISorter sorter, IAlphabet alphabet, IPage<byte> page) {
            if (sorter == null)
                throw new ArgumentNullException("Argument " + sorter.GetType().ToString() + " " + nameof(sorter) + " is null");
            if (alphabet == null)
                throw new ArgumentNullException("Argument " + nameof(IAlphabet) + " " + nameof(alphabet) + " is null");
            if (page == null)
                throw new ArgumentNullException("Argument " + nameof(IPage<byte>) + " " + nameof(page) + " is null");

            _sorter = sorter;
            _alphabet = alphabet;
            _page = page;
        }

        abstract public IPage<byte> Compose();
    }
    class PageComposer : PageComposerBase {
        protected DelegateFilter<LetterComponentDist> _delegateFilter;
        public PageComposer(ISorterWithAlphabet sorter, DelegateFilter<LetterComponentDist> delegateFilter = null)
            : base(sorter, sorter.GetAlphabet, sorter.GetPage) {
            _delegateFilter = delegateFilter;
        }
        public PageComposer(ISorter sorter, IAlphabet alphabet, IPage<byte> page, DelegateFilter<LetterComponentDist> delegateFilter = null)
            : base(sorter, alphabet, page) {
            _delegateFilter = delegateFilter;
        }

        override public IPage<byte> Compose() {
            _sorter.Sort(out var sortedComponents);
            return (new PageFactoryLetters(_page, sortedComponents, (_delegateFilter == null) ? (i => i) : _delegateFilter)).Create();
        }
    }
}
