using System.Collections.Generic;
using System.Linq;
using Chinchillada;
using JetBrains.Annotations;

namespace Chinchillada.PCG.Evolution
{
    [UsedImplicitly]
    public class WeightedParentSelector : IParentSelector
    {
        public IEnumerable<IGenotype> SelectParents(IEnumerable<IGenotype> genotypes)
        {
            var population = genotypes.ToList();
            
            while (true)
                yield return population.ChooseRandomWeighted(genotype => genotype.Fitness);
        }
    }
}