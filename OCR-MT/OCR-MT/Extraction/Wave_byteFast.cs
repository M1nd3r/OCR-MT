using OCR_MT.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Extraction {
    class Wave_byteFast :Wave<byte>{
        protected byte _target;
        public Wave_byteFast(IImage<byte> img, byte target) : base(img) {
            _target = target;
        }
        public bool GetNext(out IList<(int, int)> coords) {
            coords = null;
            for (int y = _y; y < _img.Height; y++) {
                for (int x = _x; x < _img.Width; x++) {
                    if (!_tested[x + 1, y + 1] && _img[x, y] == _target) {
                        _x = x;
                        _y = y;
                        coords = SolveComponent(x, y);
                        _tested[x + 1, y + 1] = true;
                        return true;
                    }
                }
                _x = 0;
            }
            return false;
        }
        protected IList<(int, int)> SolveComponent(int x, int y, bool includeDiagonal = false) {
            
            var    list = new List<(int x, int y)>();
            var toDo = new Queue<(int x, int y)>();
            toDo.Enqueue((x, y));
            _tested[x + 1, y + 1] = true;
            while (toDo.Count > 0) {
                (int x, int y) t = toDo.Dequeue();
                if (_img[t.x, t.y] == _target) {
                    list.Add((t.x, t.y));
                    if (!_tested[t.x, t.y + 1]) {
                        _tested[t.x, t.y + 1] = true;
                        toDo.Enqueue((t.x - 1, t.y));
                    }
                    if (!_tested[t.x + 1, t.y]) {
                        _tested[t.x + 1, t.y] = true;
                        toDo.Enqueue((t.x, t.y - 1));
                    }
                    if (!_tested[t.x + 2, t.y + 1]) {
                        _tested[t.x + 2, t.y + 1] = true;
                        toDo.Enqueue((t.x + 1, t.y));
                    }
                    if (!_tested[t.x + 1, t.y + 2]) {
                        _tested[t.x + 1, t.y + 2] = true;
                        toDo.Enqueue((t.x, t.y + 1));
                    }
                    if (includeDiagonal) {
                        if (!_tested[t.x, t.y]) {
                            _tested[t.x, t.y] = true;
                            toDo.Enqueue((t.x - 1, t.y - 1));
                        }
                        if (!_tested[t.x + 2, t.y]) {
                            _tested[t.x + 2, t.y] = true;
                            toDo.Enqueue((t.x + 1, t.y - 1));
                        }
                        if (!_tested[t.x + 2, t.y + 2]) {
                            _tested[t.x + 2, t.y + 2] = true;
                            toDo.Enqueue((t.x + 1, t.y + 1));
                        }
                        if (!_tested[t.x, t.y + 2]) {
                            _tested[t.x, t.y + 2] = true;
                            toDo.Enqueue((t.x - 1, t.y + 1));
                        }
                    }
                }
            }
            return list;
        }
    }
}
