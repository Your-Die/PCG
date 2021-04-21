namespace Chinchillada.Generation
{
    using System;
    using System.Collections.Generic;
    using Sirenix.Serialization;

    [Serializable]
    public class AsyncAdapter<T> : AsyncGeneratorBase<T>
    {
        [OdinSerialize] private IGenerator<T> generator;

        public override IEnumerable<T> GenerateAsync()
        {
            yield return this.generator.Generate();
        }
    }
}