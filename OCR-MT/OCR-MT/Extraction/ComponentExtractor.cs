using OCR_MT.Core;
using OCR_MT.Imaging;
using System.Collections.Generic;

namespace OCR_MT.Extraction {
    interface IComponentExtractor<T> {
        public IList<IComponent<T>> Extract(IImage<T> img, T target, string ID);
    }
    interface IComponentExtractor : IComponentExtractor<byte> { }
    abstract class ComponentExtractorSingle<T> : IComponentExtractor<T> {
        protected ComponentFactory<T> _cf;
        protected WaveSingleTarget<T> _wave;
        public virtual IList<IComponent<T>> Extract(IImage<T> img, T target, string ID) {
            IList<IComponent<T>> c = new List<IComponent<T>>();
            while (_wave.GetNext(out IList<(int, int)> coords)) {
                c.Add(_cf.CreateComponent(coords));
            }
            return c;
        }
    }
    class ComponentBWExtractor : ComponentExtractorSingle<byte>, IComponentExtractor {
        public override IList<IComponent<byte>> Extract(IImage<byte> img, byte target, string ID) {
            _wave = new Wave_byte(img, target);
            _cf = ComponentFactory.Get(target);
            return base.Extract(img, target, ID);
        }
    }
}
