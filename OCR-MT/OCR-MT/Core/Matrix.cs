using System;
using System.Collections;

namespace OCR_MT.Core {

    public abstract class Matrix {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
    }
    public sealed class MatrixBit : Matrix {
        private BitArray field;
        public MatrixBit(int width, int height) {
            Height = height;
            Width = width;
            this.field = new BitArray(width * height, false);
        }
        public BitArray Field => field;
        public bool this[int x, int y] {
            get => field[y * Width + x];
            set => field[y * Width + x] = value;
        }
    }
    public sealed class Matrix<T> : Matrix {
        private T[,] field;

        public Matrix(int width, int height) {
            Height = height;
            Width = width;
            this.field = new T[width, height];
        }
        public Matrix(T[,] field) {
            if (field == null)
                throw new ArgumentNullException();
            this.field = field;
            Width = field.GetLength(0);
            Height = field.GetLength(1);
        }

        public T this[int x, int y] {
            get => field[x, y];
            set => field[x, y] = value;
        }
        public static implicit operator T[,](Matrix<T> m) => m.field;
        public static implicit operator Matrix<T>(T[,] field) => new Matrix<T>(field);
        public static explicit operator Matrix<double>(Matrix<T> m) {
            Matrix<double> r = new Matrix<double>(m.Width, m.Height);
            if (m is Matrix<byte> b) {
                for (int x = 0; x < r.Width; x++) {
                    for (int y = 0; y < r.Height; y++)
                        r[x, y] = b[x, y];
                }
            }
            else if (m is Matrix<int> i) {
                for (int x = 0; x < r.Width; x++) {
                    for (int y = 0; y < r.Height; y++)
                        r[x, y] = i[x, y];
                }
            }
            else if (m is Matrix<long> l) {
                for (int x = 0; x < r.Width; x++) {
                    for (int y = 0; y < r.Height; y++)
                        r[x, y] = l[x, y];
                }
            }
            else
                throw new InvalidCastException();
            return r;
        }
    }
}

