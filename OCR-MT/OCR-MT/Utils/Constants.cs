using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_MT.Utils {
    internal static class Constants {
        internal static class Suffixes {
            internal readonly static string Png = ".png";
        }
        internal static class Colors {
            internal readonly static byte Black_byte = 0;
            internal readonly static byte Red_byte = 100;
            internal readonly static byte White_byte = 255;
        }
        internal static class Paths {
            public static class Experiments {
                internal readonly static string Output = "../../../ExperimentsOutput/";
                internal readonly static string Input = "../../../ExperimentsInput/";
                internal readonly static string OutputPages = "../../../ExperimentsOutput/Pages/";
                internal readonly static string InputPages = "../../../ExperimentsInput/Pages/";
                public static class Jakobei {
                    public static List<string> Get(int min, int max) {
                        var r = new List<string>();
                        for (int i = Math.Max(min, 1); i < 10 && i <= max; i++) {
                            r.Add(GetPathJakobei() + "00" + i.ToString() + Suffixes.Png);
                        }
                        for (int i = Math.Max(min, 10); i < 100 && i <= max; i++) {
                            r.Add(GetPathJakobei() + "0" + i.ToString() + Suffixes.Png);
                        }
                        for (int i = Math.Max(min, 100); i <= 518 && i <= max; i++) {
                            r.Add(GetPathJakobei() + i.ToString() + Suffixes.Png);
                        }
                        return r;
                    }
                    private static string GetPathJakobei() => InputPages + "/Jakobei_Convert_";

                }
            }

        }
    }
}
