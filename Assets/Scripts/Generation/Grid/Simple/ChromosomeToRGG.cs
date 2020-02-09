using System.Collections.Generic;
using System.Linq;
using Chinchillada.Colors;
using Chinchillada.Distributions;
using Chinchillada.Generation.Evolution.Chromosome;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class ChromosomeToRGG : ChromosomeInterpreter<RandomGridGenerator>
    {
        [SerializeField] private Vector2Int widthRange;
        [SerializeField] private Vector2Int heightRange;
        
        [SerializeField] private RandomGridGenerator generator;

        [SerializeField] private ColorScheme colorScheme;

        public override int ChromosomeLength => 2 + this.colorScheme.Count;
        

        protected override void Interpret(RandomGridGenerator target, Chromosome chromosome)
        {
            this.generator.Width = this.widthRange.RangeLerp(chromosome[0]);
            this.generator.Height = this.heightRange.RangeLerp(chromosome[1]);


            var distributionGenes = chromosome.Skip(2).ToArray();
            this.generator.ValueDistribution = this.BuildColorDistribution(distributionGenes);
        }

        private IDistribution<int> BuildColorDistribution(IReadOnlyList<float> probabilities)
        {
            var weights = new Dictionary<int, float>();
            for (var i = 0; i < this.colorScheme.Count; i++)
            {
                var gene = probabilities[i];
                weights[i] = gene;
            }

            return FloatWeighted<int>.Distribution(weights);
        }
    }
}