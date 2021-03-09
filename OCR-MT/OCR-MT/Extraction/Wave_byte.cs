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
                    if (!_tested[x + 1, y + 1] && _img[x, y] == _target) {
                        _x = x;
                        _y = y;
                        queue = SolveComponent(x, y);
                        _tested[x + 1, y + 1] = true;
                        return true;
                    }
                }
                _x = 0;
            }
            return false;
        }
        protected Queue<(int, int)> SolveComponent(int x, int y, bool includeDiagonal = false) {
            
            Queue<(int, int)>
                q = new Queue<(int x, int y)>(),
                toDo = new Queue<(int x, int y)>();
            toDo.Enqueue((x, y));
            _tested[x + 1, y + 1] = true;
            while (toDo.Count > 0) {
                (int x, int y) t = toDo.Dequeue();
                if (_img[t.x, t.y] == _target) {
                    q.Enqueue((t.x, t.y));
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
            return q;
        }
    }
}
