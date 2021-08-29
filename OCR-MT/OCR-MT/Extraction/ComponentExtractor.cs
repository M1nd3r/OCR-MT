using OCR_MT.Core;
using OCR_MT.Imaging;
using System.Collections.Generic;
using System.IO;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.Extraction {
    interface IComponentExtractor<T> {
        public IList<IComponent<T>> Extract(IImage<T> img, T target, string ID);
    }
    interface IComponentExtractor : IComponentExtractor<byte> { }
    abstract class ComponentExtractorFast<T> : IComponentExtractor<T> {
        protected ComponentFactory<T> _cf;
        protected Wave_byteFast _wave;
        public virtual IList<IComponent<T>> Extract(IImage<T> img, T target, string ID) {
            IList<IComponent<T>> c = new List<IComponent<T>>();
            var path = Paths.Experiments.Output + "Components/";
            Directory.CreateDirectory(path);
            while (_wave.GetNext(out IList<(int, int)> list)) {
                c.Add(_cf.CreateComponent(list));
            }
            return c;
        }
    }
    abstract class ComponentExtractorSingle<T> : IComponentExtractor<T> {
        protected ComponentFactory<T> _cf;
        protected WaveSingleTarget<T> _wave;
        public virtual IList<IComponent<T>> Extract(IImage<T> img, T target, string ID) {
            IList<IComponent<T>> c = new List<IComponent<T>>();
            var path = Paths.Experiments.Output + "Components/";
            Directory.CreateDirectory(path);
            while (_wave.GetNext(out Queue<(int, int)> q)) {
                c.Add(_cf.CreateComponent(q));
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
    class ComponentBWExtractorFast : ComponentExtractorFast<byte>, IComponentExtractor {
        public override IList<IComponent<byte>> Extract(IImage<byte> img, byte target, string ID) {
            _wave = new Wave_byteFast(img, target);
            _cf = ComponentFactory.Get(target);
            return base.Extract(img, target, ID);
        }
    }
}
