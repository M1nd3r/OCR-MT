using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCR_MT.Core;
using OCR_MT.Extraction;
using OCR_MT.Imaging;
using System;
using static OCR_MT.Utils.Constants.Colors;
using static OCR_MT.Utils.Extensions;


namespace OCR_MT_Tests {
    [TestClass]
    public class Wave {
        
        [TestMethod]
        public void CreationTestNullException() {
            Assert.ThrowsException<ArgumentNullException>(CreateNullWave);            
        }
        private void CreateNullWave() {
            Wave_byte v = new Wave_byte(null, Black_byte);
        }

        [TestMethod]
        public void CreateWave() {
            var img = GetMatrix(2, 2);
            Wave_byte v = new Wave_byte(img, Black_byte);
        }

        [TestMethod]
        public void SinglePixelWave() {
            var img = GetMatrix(1, 1);
            Wave_byte v = new Wave_byte(img, Black_byte);
            v.GetNext(out var a);
            Assert.IsTrue(a.Count == 1);
            Assert.IsTrue(a[0] == (0, 0));
        }
        [TestMethod]
        public void GetNonExistentComponent() {
            var img = GetMatrix(3, 3);
            Wave_byte v = new Wave_byte(img, White_byte);
            Assert.IsFalse(v.GetNext(out var notUsedVariable));
        }
        [TestMethod]
        public void GetCorrectSizeOfTwoByTwoComponent() {
            var m = GetWhiteMatrixWithBlackTwoByTwoSquareInTheLeftUpperCorner();
            Wave_byte w = new Wave_byte(m, Black_byte);
            w.GetNext(out var firstComponent);
            Assert.IsTrue(firstComponent.Count == 4);
        }
        [TestMethod]
        public void GetCorrectSizeOfTwoByOneComponent() {
            var m = GetWhiteMatrix(4, 4);
            m[0, 0] = Black_byte;
            m[0, 1] = Black_byte;
            Wave_byte w = new Wave_byte(m, Black_byte);
            w.GetNext(out var firstComponent);
            Assert.IsTrue(firstComponent.Count == 2);
        }
        [TestMethod]
        public void GetCorrectSizeOfOneByTwoComponent() {
            var m = GetWhiteMatrix(4, 4);
            m[0, 0] = Black_byte;
            m[1,0] = Black_byte;
            Wave_byte w = new Wave_byte(m, Black_byte);
            w.GetNext(out var firstComponent);
            Assert.IsTrue(firstComponent.Count == 2);
        }
        [TestMethod]
        public void GetCorrectSizeOfCrossComponent() {
            var m = GetWhiteMatrix(3, 3);
            m[1, 0] = Black_byte;
            m[1, 1] = Black_byte;
            m[1, 2] = Black_byte;
            m[0, 1] = Black_byte;
            m[2, 1] = Black_byte;
            Wave_byte w = new Wave_byte(m, Black_byte);
            w.GetNext(out var firstComponent);
            Assert.IsTrue(firstComponent.Count == 5);
        }
        [TestMethod]
        public void GetCorrectSizeOfParnicekComponent() {
            var m = GetWhiteMatrix(3, 3);
            m[1, 0] = Black_byte;
            m[1, 1] = Black_byte;
            m[1, 2] = Black_byte;
            m[0, 1] = Black_byte;
            Wave_byte w = new Wave_byte(m, Black_byte);
            w.GetNext(out var firstComponent);
            Assert.IsTrue(firstComponent.Count == 4);
        }
        [TestMethod]
        public void GetFirstComponentFromOneComponentPicture() {
            var m = GetWhiteMatrixWithBlackTwoByTwoSquareInTheLeftUpperCorner();
            Wave_byte w = new Wave_byte(m, Black_byte);
            Assert.IsTrue(w.GetNext(out var firstComponent));
        }
        [TestMethod]
        public void GetSecondComponentFromOneComponentPicture() {
            var m = GetWhiteMatrixWithBlackTwoByTwoSquareInTheLeftUpperCorner();
            Wave_byte w = new Wave_byte(m, Black_byte);
            w.GetNext(out var firstComponent);
            Assert.IsFalse(w.GetNext(out var secondComponent));
        }


        private MatrixBW GetMatrix(int width, int height) => new MatrixBW(width, height);
        private MatrixBW GetWhiteMatrix(int width, int height) {
            var m = new MatrixBW(width, height);
            m.SetAllToMax();
            return m;
        }
        private MatrixBW GetWhiteMatrixWithBlackTwoByTwoSquareInTheLeftUpperCorner() {
            var m = GetWhiteMatrix(4, 4);
            m[0, 0] = Black_byte;
            m[0, 1] = Black_byte;
            m[1, 0] = Black_byte;
            m[1, 1] = Black_byte;
            return m;
        }
    }
}
