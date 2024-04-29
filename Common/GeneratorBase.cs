using System;

namespace Chinchillada.PCG
{
    using Chinchillada;

    public abstract class GeneratorBase<T> : Strategy, IGenerator<T>
    {
        public T Result { get; private set; }

        public event Action<T> Generated;

        public T Generate(IRNG random)
        {
            this.OnBeforeGenerate();
            
            var result = this.GenerateInternal(random);
            
            this.RegisterResult(result);
            return this.Result;
        }

        protected abstract T GenerateInternal(IRNG random);

        protected void RegisterResult(T result)
        {
            this.Result = result;
            this.Generated?.Invoke(this.Result);
        }

        protected virtual void OnBeforeGenerate()
        {
        }

    }
}