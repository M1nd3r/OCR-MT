using System;
using static OCR_MT.Core.Metrics.CleverComponents;

namespace OCR_MT.Core.Identification {
    interface ICleverComponent {
        public double Distance(ICleverComponent component);
        public IComponent<byte> GetComponent { get; }
        public double MinDistance { get; set; }
    }
    class CleverComponent : ICleverComponent {
        protected IComponent<byte> _component;
        public CleverComponent(IComponent<byte> component) {
            _component = component;
        }
        public virtual double Distance(ICleverComponent component) {
            return SizeMetric(this, component);
        }
        public IComponent<byte> GetComponent { get => _component; }
        public double MinDistance { get; set; }
    }
    class CleverComponentThumbnail : CleverComponent {
        protected Matrix<double> thumbnail;
        protected int width, height;
        public CleverComponentThumbnail(IComponent<byte> component) : base(component) {
            CreateComponentThumbnail();
        }
        protected void CreateComponentThumbnail() {
            width = 6;
            height = 6;
            CreateComponentThumbnailWithGivenSize();
        }
        protected void CreateComponentThumbnailWithGivenSize() {
            if (_component is ComponentBW_byte cByte) {
                thumbnail = MatrixMethods.Approximate(cByte.Matrix, width, height);
                return;
            }
            if (_component is ComponentBW_bit cBit) {
                thumbnail = MatrixMethods.Approximate(cBit.Matrix, width, height);
                return;
            }
            throw new InvalidOperationException("The component is not of a known (supported) type bit/byte");
        }
    }
}
