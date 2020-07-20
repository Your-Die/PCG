using System;

namespace Chinchillada.Generation
{
    public abstract class GeneratorBase<T> : IGenerator<T>
    {
        public T Result { get; private set; }

        public event Action<T> Generated;

        public T Generate()
        {
            var result = this.GenerateInternal();
            
            this.RegisterResult(result);
         
            return this.Result;
        }

        protected abstract T GenerateInternal();

        protected void RegisterResult(T result)
        {
            this.Result = result;
            this.Generated?.Invoke(this.Result);
        }
    }
}