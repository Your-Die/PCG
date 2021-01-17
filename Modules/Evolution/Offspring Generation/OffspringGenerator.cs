using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable]
    public class OffspringGenerator<T> :  IOffspringGenerator<T>
    {
        [SerializeField] private ICrossover<T> crossover;

        [SerializeField] private List<Mutator<T>> mutators = new List<Mutator<T>>();

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

                    yield return this.crossover.Crossover(parent1, parent2, random);
                }
            }
        }
    }
}