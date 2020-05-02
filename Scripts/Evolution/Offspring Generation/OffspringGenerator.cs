using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = Chinchillada.Utilities.Random;

namespace Chinchillada.Generation.Evolution
{
    [Serializable]
    public class OffspringGenerator<T> :  IOffspringGenerator<T>
    {
        [SerializeField] private ICrossover<T> crossover;

        [SerializeField] private List<Mutator<T>> mutators = new List<Mutator<T>>();

        public IEnumerable<T> GenerateOffspring(IEnumerable<T> candidates, int amount)
        {
            if (this.crossover != null) 
                candidates = this.Crossover(candidates);

            return candidates.Select(this.Mutate).Take(amount);
        }

        private IEnumerable<T> Crossover(IEnumerable<T> parents)
        {
            using (var enumerator = parents.GetEnumerator())
            {
                var anyLeft = enumerator.MoveNext();

                while (anyLeft)
                {
                    var parent1 = enumerator.Current;
                    anyLeft = enumerator.MoveNext();
                    
                    if (!anyLeft)
                        yield break;

                    var parent2 = enumerator.Current;
                    anyLeft = enumerator.MoveNext();

                    yield return this.crossover.Crossover(parent1, parent2);
                }
            }
        }

        private T Mutate(T candidate)
        {
            foreach (var mutator in this.mutators)
            {
                if (Random.Bool(mutator.Chance))
                    return mutator.Mutate(candidate);
            }

            return candidate;
        }
    }
}