using OCR_MT.Imaging;
using System;
using System.Collections.Generic;

namespace OCR_MT.Extraction {
    class Wave_byte : WaveSingleTarget<byte> {
        private Queue<(int x, int y)> toEvaluate;
        private bool includeDiagonal = false;
        private delegate void PositionsSolver(int x, int y);
        private PositionsSolver _positionsSolver;
        public bool IncludeDiagonal { get => includeDiagonal; set => includeDiagonal = value; }

        public Wave_byte(IImage<byte> img, byte target) : base(img, target) { }
        public override bool GetNext(out IList<(int, int)> coords) {
            coords = null;
            _positionsSolver = GetPositionsSolver();
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
        private PositionsSolver GetPositionsSolver() {
            if (includeDiagonal)
                return SolveAllPositions;
            return SolveBasicPositions;
        }
        protected IList<(int, int)> SolveComponent(int x, int y) {
            toEvaluate = new Queue<(int x, int y)>();
            toEvaluate.Enqueue((x, y));
            _tested[x + 1, y + 1] = true;
            var q = new List<(int x, int y)>();
            while (toEvaluate.Count > 0) {
                (int x, int y) t = toEvaluate.Dequeue();
                if (_img[t.x, t.y] == _target) {
                    q.Add((t.x, t.y));
                    _positionsSolver(x, y);
                }
            }
            return q;
        }
        private void SolveAllPositions(int x, int y) {
            SolveBasicPositions(x, y);
            SolveDiagonalPositions(x, y);
        }
        private void SolveBasicPositions(int x, int y) {
            SolvePosition(x, y + 1);
            SolvePosition(x + 1, y);
            SolvePosition(x + 2, y + 1);
            SolvePosition(x + 1, y + 2);
        }
        private void SolveDiagonalPositions(int x, int y) {
            SolvePosition(x, y);
            SolvePosition(x + 2, y);
            SolvePosition(x, y + 2);
            SolvePosition(x + 2, y + 2);
            
        }
        private void SolvePosition(int x, int y) {
            if(!_tested[x, y]) {
                _tested[x, y] = true;
                toEvaluate.Enqueue((x - 1, y - 1));
            }
        }
    }
}
