using System;
using Chinchillada.Distributions;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable]
    public class MutatorComposite<T> : Mutator<T>
    {
        [SerializeField] private IDistribution<Mutator<T>> mutatorDistribution;
        
        public override T Mutate(T parent, IRNG random)
        {
            var mutator = this.mutatorDistribution.Sample(random);
            return mutator.Mutate(parent, random);
        }
    }
}