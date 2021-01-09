namespace Chinchillada.Generation
{
    using System.Collections.Generic;
    using UnityEngine;

    public class AsyncGeneratorComponent<T> : AsyncGeneratorComponentBase<T>
    {
        [SerializeField] private IAsyncGenerator<T> generator;
        
        public override IEnumerable<T> GenerateAsync() => this.generator.GenerateAsync();
    }
}