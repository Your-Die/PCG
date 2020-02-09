using Chinchillada.Distributions;
using UnityEngine;

namespace Chinchillada.Generation.Evolution.Chromosome
{
    public class ChromosomeJitter : GeneMutator
    {
        [SerializeField] private FloatDistribution jitterDistribution;

        protected override float MutateGene(float gene)
        {
            gene += this.jitterDistribution.Sample();
            return Mathf.Clamp01(gene);
        }
    }
}