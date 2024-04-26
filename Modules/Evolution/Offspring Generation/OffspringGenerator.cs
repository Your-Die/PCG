using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.PCG.Evolution
{
    using Sirenix.Serialization;

    [Serializable]
    public class OffspringGenerator<T> :  IOffspringGenerator<T>
    {
        [SerializeField] private ICrossover<T> crossover;

        [SerializeField] private List<WeightedMutator<T>> mutators = new List<WeightedMutator<T>>();

        public IEnumerable<T> GenerateOffspring(IEnumerable<T> candidates, int amount, IRNG random)
        {
            if (this.crossover != null) 
                candidates = this.Crossover(candidates, random);

            return candidates.Select(Mutate).Take(amount);
            
            T Mutate(T candidate)
            {
                foreach (var mutator in this.mutators)
                {
                    if (random.Flip(mutator.Chance))
                        return mutator.Mutate(candidate, random);
                }

                return candidate;
            }
        }

        private IEnumerable<T> Crossover(IEnumerable<T> parents, IRNG random)
        {
            using var enumerator = parents.GetEnumerator();
            var       anyLeft    = enumerator.MoveNext();

            while (anyLeft)
            {
                var parent1 = enumerator.Current;
                anyLeft = enumerator.MoveNext();
                    
                if (!anyLeft)
                    yield break;

                var parent2 = enumerator.Current;
                anyLeft = enumerator.MoveNext();

                yield return this.crossover.Crossover(parent1, parent2, random);
            }
        }
        
        [Serializable]
        private class WeightedMutator<T> : IMutator<T>
        {
            [SerializeField, Range(0, 1)] private float chance;

            [OdinSerialize] private IMutator<T> mutator;

            public float Chance => this.chance;

            public T Mutate(T parent, IRNG random)
            {
                return this.mutator.Mutate(parent, random);
            }
        }
    }
}