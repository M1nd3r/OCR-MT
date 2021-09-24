namespace OCR_MT.Core.Identification {
    interface ICleverComponentFactory {
        public ICleverComponent Create(IComponent<byte> component);
    }
    class CleverComponentFactory : ICleverComponentFactory {
        public ICleverComponent Create(IComponent<byte> component) {
            return new CleverComponent(component);
        }
    }
}
