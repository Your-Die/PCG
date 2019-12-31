using System;
using Chinchillada;
using Chinchillada.Generation;
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

    }
}