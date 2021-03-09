using OCR_MT.Imaging;
using System;
using System.Collections.Generic;

namespace OCR_MT.Extraction {
    class Wave_byte : WaveSingleTarget<byte> {
        public Wave_byte(IImage<byte> img, byte target) : base(img, target) { }
        public override bool GetNext(out Queue<(int, int)> queue) {
            queue = null;
            for (int y = _y; y < _img.Height; y++) {
                for (int x = _x; x < _img.Width; x++) {
                    if (!_tested[x, y] && _img[x, y] == _target) {
                        queue = SolveComponent(x, y);
                    }
                }
            }
            if (queue == null)
                return false;
            return true;
        }
        protected Queue<(int, int)> SolveComponent(int x, int y) {
            //TODO - do not forget to set _x, _y and _tested
            Queue<(int, int)> q = new Queue<(int, int)>();
            return q;
        }
    }
}
