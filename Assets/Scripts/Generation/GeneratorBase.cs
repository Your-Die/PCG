using System;
using Chinchillada;
using Chinchillada.Generation;
using Chinchillada.Generation.Grid;
using Sirenix.OdinInspector;

namespace DefaultNamespace
{
    public abstract class GeneratorBase<T> : ChinchilladaBehaviour, IObservableGenerator<T>
    {
        public event Action<T> Generated;

        [Button]
        public T Generate()
        {
            var item = this.GenerateInternal();
            
            this.Generated?.Invoke(item);
            return item;
        }

        protected abstract T GenerateInternal();

        protected void OnGenerated(T result) => this.Generated?.Invoke(result);
    }
}