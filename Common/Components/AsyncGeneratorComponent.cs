namespace Chinchillada.Generation
{
    using System.Collections.Generic;
    using Sirenix.Serialization;

    public class AsyncGeneratorComponent<T> : AsyncGeneratorComponentBase<T>
    {
        [OdinSerialize] private IAsyncGenerator<T> generator;
        
        public override IEnumerable<T> GenerateAsync() => this.generator.GenerateAsync();
    }
}