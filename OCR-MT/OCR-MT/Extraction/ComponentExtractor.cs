using OCR_MT.Core;
using OCR_MT.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.Extraction {
    abstract class ComponentExtractorSingle<T> {
        protected ComponentFactory<T> _cf;
        protected WaveSingleTarget<T> _wave;
        public virtual IList<IComponent<T>> Extract(IImage<T> img, T target, string ID) {
            IList<IComponent<T>> c = new List<IComponent<T>>();
            var path = Paths.Experiments.Output + "Components/";
            Directory.CreateDirectory(path);
            IO.ImageBWSaver.Save(img as IImage<byte>, path+"page_"+ID+Suffixes.Png);
            while (_wave.GetNext(out Queue<(int,int)> q)) {
                c.Add(_cf.CreateComponent(q));
                IO.ImageBWSaver.Save(c[c.Count - 1] as IImage<byte>, path+ID +"_"+ c.Count.ToString() + Suffixes.Png);
            }
            return c;
        }
    }
    class ComponentBWExtractor : ComponentExtractorSingle<byte> {        
        public override IList<IComponent<byte>> Extract(IImage<byte> img, byte target, string ID) {
            _wave = new Wave_byte(img, target);
            _cf = ComponentFactory.Get(target);
            return base.Extract(img, target, ID);
        }
    }
}
