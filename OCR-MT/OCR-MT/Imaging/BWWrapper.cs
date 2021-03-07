using SixLabors.ImageSharp.PixelFormats;
using static OCR_MT.Utils.Constants;

namespace OCR_MT.Imaging {
    struct BWWrapper : BW {
        private byte red;
        public BWWrapper(Rgba32 rgba32) {
            red = rgba32.R;
        }
        public byte Intensity => (red > 0) ? Colors.White_byte : Colors.Black_byte;
    }
}
