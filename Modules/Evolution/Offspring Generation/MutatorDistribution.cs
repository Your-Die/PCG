using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Distributions;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable]
    public class MutatorDistribution<T> : IWeightedDistribution<Mutator<T>>
    {
        [SerializeField] private IList<Mutator<T>> mutators;
        
        private IWeightedDistribution<Mutator<T>> distribution;
        
        public Mutator<T> Sample()
        {
            this.EnsureDistribution();
            return this.distribution.Sample();
        }

        public float Weight(Mutator<T> item)
        {
            this.EnsureDistribution();
            return this.distribution.Weight(item);
        }

        private void EnsureDistribution()
        {
            if (this.distribution != null)
                return;

            var weightDictionary = this.mutators.ToDictionary(
                mutator => mutator,
                mutator => mutator.Chance);

            this.distribution = FloatWeighted<Mutator<T>>.Distribution(weightDictionary);
        }
    }
}