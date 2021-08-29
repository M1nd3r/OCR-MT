using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Core.Identification
{
    interface IComponentSortHandler
    {
        public IAlphabet Mutate(in IAlphabet alphabet);
        public IComponent<byte> Mutate(in IComponent<byte> component);
        public double Distance(in ILetter letter, in IComponent<byte> component);
    }
}
