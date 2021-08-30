using System;
using OCR_MT.Core;

namespace OCR_MT.Utils {
    internal static class Extensions {
        public static void SetAllToFalse(this bool[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    arr[x, y] = false;
                }
            }
        }
        public static void SetAllToFalse(this bool[] arr) {
            for (int x = 0; x < arr.Length; x++)
                arr[x] = false;
        }
        public static void SetAllToTrue(this bool[] arr) {
            for (int x = 0; x < arr.Length; x++)
                arr[x] = true;
        }
        public static void SetFrameToTrueInsideToFalse(this bool[,] arr) {
            for (int y = 1; y < arr.GetLength(1) - 1; y++) {
                for (int x = 1; x < arr.GetLength(0) - 1; x++) {
                    arr[x, y] = false;
                }
            }
            for (int x = 0; x < arr.GetLength(0); x++) {
                arr[x, 0] = true;
                arr[x, arr.GetLength(1) - 1] = true;
            }
            for (int y = 1; y < arr.GetLength(1) - 1; y++) {
                arr[0, y] = true;
                arr[arr.GetLength(0) - 1, y] = true;
            }
        }
        public static double Max(this double[] arr) {
            double r = Double.NegativeInfinity;
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] > r)
                    r = arr[i];
            }
            return r;
        }
        public static void SetAllToZero(this byte[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    arr[x, y] = 0;
                }
            }
        }
        public static void SetAllToZero(this int[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    arr[x, y] = 0;
                }
            }
        }
        public static void SetAllToZero(this int[] arr) {
            for (int x = 0; x < arr.Length; x++)
                arr[x] = 0;

        }
        public static int Sum(this int[] arr) {
            int r = 0;
            for (int x = 0; x < arr.Length; x++)
                r += arr[x];
            return r;
        }
        public static void SetAllToZero(this double[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    arr[x, y] = 0;
                }
            }
        }
        /*
        public static void PrintToConsole(this int[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    Console.Write(arr[x, y] + " ");
                }
                Console.WriteLine();
                throw new Exception();
            }
        }
        */
        public static void SetAllToZero(this Matrix<int> m) {
            for (int x = 0; x < m.Width; x++) {
                for (int y = 0; y < m.Height; y++) {
                    m[x, y] = 0;
                }
            }
        }
        public static void SetAllToZero(this Matrix<byte> m) {
            for (int x = 0; x < m.Width; x++) {
                for (int y = 0; y < m.Height; y++) {
                    m[x, y] = 0;
                }
            }
        }
        public static void SetAllToMax(this Matrix<byte> m) {
            for (int x = 0; x < m.Width; x++) {
                for (int y = 0; y < m.Height; y++) {
                    m[x, y] = 255;
                }
            }
        }
        public static void SetAllToZero(this Matrix<double> m) {
            for (int x = 0; x < m.Width; x++) {
                for (int y = 0; y < m.Height; y++) {
                    m[x, y] = 0;
                }
            }
        }
        public static void PrintToConsoleBlocks(this byte[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    if (arr[x, y] <= 0)
                        Console.Write("_");
                    else
                        Console.Write("▓");
                }
                Console.WriteLine();
            }
        }
        public static void PrintToConsoleBlocks(this double[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    if (arr[x, y] < 0.2)
                        Console.Write("_");
                    else if (arr[x, y] < 0.5)
                        Console.Write("░");
                    else if (arr[x, y] < 0.8)
                        Console.Write("▒");
                    else
                        Console.Write("▓");

                }
                Console.WriteLine();
            }
        }
        public static void PrintAsBWImage(this int[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    if (arr[x, y] > 0)
                        Console.Write("X");
                    else
                        Console.Write("0");
                }
                Console.WriteLine();
            }
        }
        public static void PrintAsBWImage(this byte[,] arr) {
            for (int y = 0; y < arr.GetLength(1); y++) {
                for (int x = 0; x < arr.GetLength(0); x++) {
                    if (arr[x, y] > 0)
                        Console.Write("X");
                    else
                        Console.Write("0");
                }
                Console.WriteLine();
            }
        }
        public static void PrintToConsole(this bool[,] arr) {
            for (int x = 0; x < arr.GetLength(0); x++) {
                for (int y = 0; y < arr.GetLength(1); y++) {
                    if (arr[x, y])
                        Console.Write("1");
                    else
                        Console.Write("0");
                }
                Console.WriteLine();
            }
        }
        public static double Distance(this (int x1, int y1) tuple, int x2, int y2) => Math.Sqrt(((tuple.x1 - x2) * (tuple.x1 - x2) + (tuple.y1 - y2) * (tuple.y1 - y2)));
    }
}
