using System;
using System.Collections.Generic;

namespace OCR_MT.Core.Identification {
    class PageComponentSorter : ICleverSorter {
        private IList<ICleverComponent> measurableComponents;
        private IList<IList<ICleverComponent>> sortedMeasurableComponents;
        private IAlphabet alphabet;
        private IList<ICleverComponent> measurableAlphabet;
        private ICleverComponentFactory componentFactory = new CleverComponentFactory();
        private LetterBWFactory letterFactory = new LetterBWFactory();
        public PageComponentSorter(IAlphabet alphabet) {
            this.alphabet = alphabet;
            ChangeAlphabetToMeasurable();
        }
        public IList<IList<ILetter>> Sort(IPage<byte> page) {
            ChangeComponentsToMeasurable(page.Components);
            SortMeasurableComponents();
            var result = CreateLettersFromSortedComponents();
            Clean();
            return result;
        }
        private void ChangeComponentsToMeasurable(IList<IComponent<byte>> components) {
            measurableComponents = ChangeToMeasurable(components);
        }
        private void ChangeAlphabetToMeasurable() {
            measurableAlphabet = ChangeToMeasurable(alphabet.GetLetters);
        }
        private IList<ICleverComponent> ChangeToMeasurable<T>(IList<T> components) where T : IComponent<byte> {
            IList<ICleverComponent> result = new List<ICleverComponent>();
            foreach (var c in components)
                result.Add(componentFactory.Create(c));
            return result;
        }
        private IList<IList<T>> InitializeOutputList<T>(int length) {
            var r = new List<IList<T>>(length);
            for (int i = 0; i < length; i++) {
                r.Add(new List<T>());
            }
            return r;
        }
        private void SortMeasurableComponents() {
            sortedMeasurableComponents = InitializeOutputList<ICleverComponent>(alphabet.GetLetters.Count);
            foreach (var component in measurableComponents) {
                SortMeasurableComponent(component);
            }
        }
        private void SortMeasurableComponent(ICleverComponent component) {
            double minDist = Int32.MaxValue;
            int index = -1;
            for (int i = 0; i < measurableAlphabet.Count; i++) {
                double dist = component.Distance(measurableAlphabet[i]);
                if (dist < minDist) {
                    minDist = dist;
                    index = i;
                }
                component.MinDistance = minDist;
            }
            if (index < 0)
                throw new Exception("Component was not close to any letter of alphabet or the alphabet is empty");
            sortedMeasurableComponents[index].Add(component);
        }
        private IList<IList<ILetter>> CreateLettersFromSortedComponents() {
            var sortedLetters = InitializeOutputList<ILetter>(alphabet.GetLetters.Count);
            for (int i = 0; i < sortedMeasurableComponents.Count; i++) {
                foreach (var component in sortedMeasurableComponents[i]) {
                    sortedLetters[i].Add(new LetterComponentDist(alphabet[i], component.GetComponent,component.MinDistance));
                }
            }
            return sortedLetters;
        }
        private void Clean() {
            measurableComponents = null;
            sortedMeasurableComponents = null;
        }
    }
}
