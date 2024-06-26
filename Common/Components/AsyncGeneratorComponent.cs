﻿namespace Chinchillada.PCG
{
    using System.Collections.Generic;
    using Sirenix.Serialization;

    public class AsyncGeneratorComponent<T> : AsyncGeneratorComponentBase<T>
    {
        [OdinSerialize] private IAsyncGenerator<T> generator;
        
        public override IEnumerable<T> GenerateAsync(IRNG random) => this.generator.GenerateAsync(random);
    }
}