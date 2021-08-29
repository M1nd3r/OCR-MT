using OCR_MT.Imaging;
using OCR_MT.Utils;
using System.Collections.Generic;


namespace OCR_MT.Extraction {
    abstract class Wave<T> {
        protected IImage<T> _img;
        protected bool[,] _tested;
        protected int _x, _y;
        public Wave(IImage<T> img) {
            _img = img;
            _x = 0;
            _y = 0;
            _tested = new bool[img.Width + 2, img.Height + 2];
            _tested.SetFrameToTrueInsideToFalse();
        }       
    }
    
    abstract class WaveSingleTarget<T> : Wave<T>{
        protected T _target;
        public WaveSingleTarget(IImage<T> img, T target) : base(img) {
            _target = target;
        }
        public abstract bool GetNext(out IList<(int, int)> queue);        
    }
}
