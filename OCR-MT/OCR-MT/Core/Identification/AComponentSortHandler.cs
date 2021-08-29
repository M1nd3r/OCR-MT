using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OCR_MT.Utils.Delegates;

namespace OCR_MT.Core.Identification
{
    abstract class AComponentSortHandler : IComponentSortHandler
    {
        protected readonly DelegateMetric<ILetter, IComponent<byte>> _metricDelegate;
        public AComponentSortHandler(DelegateMetric<ILetter, IComponent<byte>> metricDelegate)
        {
            _metricDelegate = metricDelegate;
        }
        public double Distance(in ILetter letter,in IComponent<byte> component) => _metricDelegate(letter, component);

        public abstract IAlphabet Mutate(in IAlphabet alphabet);
        public abstract IComponent<byte> Mutate(in IComponent<byte> component);
    }
    class ComponentSortHandlerDummy : AComponentSortHandler
    {
        public ComponentSortHandlerDummy(DelegateMetric<ILetter, IComponent<byte>> metricDelegate) : base(metricDelegate) { }
       
        public override IAlphabet Mutate(in IAlphabet alphabet)
        {
            IList<ILetter> letters = new List<ILetter>();
            foreach (var letter in alphabet.GetLetters)
                letters.Add(new LetterBW(letter.LetterID, letter.Translation, Mutate(letter.LetterComponent)));            
            return new Alphabet(letters);
        }

        public override IComponent<byte> Mutate(in IComponent<byte> component)
        {
            return component;
        }
    }
    class ComponentHandlerWithBenefits : ComponentSortHandlerDummy {
        public ComponentHandlerWithBenefits(DelegateMetric<ILetter, IComponent<byte>> metricDelegate) : base(metricDelegate) { }
        public override IComponent<byte> Mutate(in IComponent<byte> component) {
            IComponent<byte> a = null; //TODO IComponent with benefits
            return a;
        }
    }
}
