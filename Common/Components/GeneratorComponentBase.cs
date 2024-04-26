using System;
using Chinchillada;

namespace Chinchillada.PCG
{
    public abstract class GeneratorComponentBase<T> : AutoRefBehaviour, IGenerator<T>
    {
        public T Result { get; private set; }
        
        public event Action<T> Generated;
        
        public T Generate()
        {
            this.Result = this.GenerateInternal();
            this.Generated?.Invoke(this.Result);

            return this.Result;
        }

        protected abstract T GenerateInternal();
    }
}