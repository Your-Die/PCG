using System.Collections.Generic;
using System.Linq;
using Chinchillada.Distributions;
using Chinchillada.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chinchillada.Generation.Evolution
{
    public class StringEvolution : EvolutionaryStrategy<string>
    {
        private IDistribution<bool> MutationFlip;

        public StringEvolution(int eliteCount, int offspringCount, float mutationChance)
            : base(eliteCount, offspringCount)
        {
            this.MutationFlip = Flip.Boolean(mutationChance);
        }

        protected override IEnumerable<string> GenerateOffspring(IEnumerable<Individual> elite, int offspringCount)
        {
            var parentDistribution = elite.ToWeighted(individual => individual.Fitness);

            for (var i = 0; i < offspringCount; i++)
            {
                var parent1 = parentDistribution.Sample();
                var parent2 = parentDistribution.Sample();

                var offspring = this.GenerateOffspring(parent1.Genotype, parent2.Genotype);
                if (this.MutationFlip.Sample())
                    offspring = this.Mutate(offspring);

                yield return offspring;
            }
        }

        protected override int EvaluateFitness(string individual)
        {
            return individual.Count(character => character == 'A');
        }

        private string GenerateOffspring(string parent1, string parent2)
        {
            var length = Mathf.Min(parent1.Length, parent2.Length);
            var crossover = Random.Range(0, length);

            var sub1 = parent1.Substring(0, crossover);
            var sub2 = parent2.Substring(crossover);

            return sub1 + sub2;
        }
        
        private string Mutate(string offspring)
        {
            var index = offspring.ChooseRandomIndex();
            var character = Random.value > 0.5f ? "A" : "a";

            var before = offspring.Substring(0, index);
            var after = offspring.Substring(index + 1);

            return before + character + after;
        }
    }
}