using static OCR_MT.Core.Metrics.CleverComponents;

namespace OCR_MT.Core.Identification {
    interface ICleverComponent {
        public double Distance(ICleverComponent component);
        public IComponent<byte> GetComponent { get; }
        public double MinDistance { get; set; }
    }
    class CleverComponent : ICleverComponent {
        private IComponent<byte> _component;
        public CleverComponent(IComponent<byte> component) {
            _component = component;
        }
        public virtual double Distance(ICleverComponent component) {
            return SizeMetric(this, component);
        }
        public IComponent<byte> GetComponent { get => _component; }
        public double MinDistance { get; set; }
    }
    interface ICleverComponentFactory {
        public ICleverComponent Create(IComponent<byte> component);
    }
    class CleverComponentFactory : ICleverComponentFactory {
        public ICleverComponent Create(IComponent<byte> component) {
            return new CleverComponent(component);
        }
    }
}
