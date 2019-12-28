using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Chinchillada.Generation.Evolution
{
    public abstract class EvolutionaryStrategy<TGenotype>
    {
        public int EliteCount { get; }

        public int OffspringCount { get; }
        
        public EvolutionaryStrategy(int eliteCount, int offspringCount)
        {
            this.EliteCount = eliteCount;
            this.OffspringCount = offspringCount;
        }

        public TGenotype EvolveGenerations(IEnumerable<TGenotype> initialPopulation, int generationCount)
        {
            var firstGeneration = this.EvaluatePopulation(initialPopulation);
            var lastGeneration = this.EvolveGenerations(firstGeneration, generationCount);

            return GetFittest(lastGeneration);
        }

        public IEnumerable<(TGenotype, float)> EvolveGenerationsStepwise(IEnumerable<TGenotype> initialPopulation,
            int generationCount)
        {
            var firstGeneration = this.EvaluatePopulation(initialPopulation);
            var fittestOfEachGeneration = this.EvolveGenerationsStepwise(firstGeneration, generationCount);
            
            foreach (var fittestOfGeneration in fittestOfEachGeneration)
                yield return fittestOfGeneration;
        }
        
        public IEnumerable<TGenotype> EvolveGeneration(IEnumerable<TGenotype> genotypes)
        {
            var generation = this.EvaluatePopulation(genotypes);
            var nextGeneration = this.EvolveGeneration(generation);
            
            return nextGeneration.Select(individual => individual.Genotype);
        }

        protected abstract IEnumerable<TGenotype> GenerateOffspring(IEnumerable<Individual> elite, int offspringCount);

        protected abstract int EvaluateFitness(TGenotype individual);

        private IEnumerable<Individual> EvaluatePopulation(IEnumerable<TGenotype> population)
        {
            return from individual in population
                let fitness = this.EvaluateFitness(individual)
                select new Individual(individual, fitness);
        }
        
        private IEnumerable<Individual> EvolveGenerations(IEnumerable<Individual> generation, int generationCount)
        {
            for (var i = 0; i < generationCount; i++) 
                generation = this.EvolveGeneration(generation);

            return generation;
        }

        private IEnumerable<(TGenotype, float)> EvolveGenerationsStepwise(IEnumerable<Individual> generation,
            int generationCount)
        {
            for (var i = 0; i < generationCount; i++)
            {
                generation = this.EvolveGeneration(generation).ToList();
                var fittest = GetFittestIndividual(generation);

                yield return fittest.ToTuple();
            }
        }
        
        private IEnumerable<Individual> EvolveGeneration(IEnumerable<Individual> individuals)
        {
            var sortedByFitness = individuals.OrderByDescending(individual => individual.Fitness);
            var elite = sortedByFitness.Take(this.EliteCount).ToList();

            var offspring = this.GenerateOffspring(elite, this.EliteCount);
            var evaluatedOffspring = this.EvaluatePopulation(offspring);

            return elite.Concat(evaluatedOffspring);
        }
        
        
        private static TGenotype GetFittest(IEnumerable<Individual> population)
        {
            var fittestIndividual = GetFittestIndividual(population);
            return fittestIndividual.Genotype;
        }

        private static Individual GetFittestIndividual(IEnumerable<Individual> population)
        {
            var orderedByFitness = population.OrderByDescending(individual => individual.Fitness);
            return orderedByFitness.First();
        }

        protected class Individual
        {
            public TGenotype Genotype { get; }
            public int Fitness { get; }

            public Individual(TGenotype genotype, int fitness)
            {
                this.Genotype = genotype;
                this.Fitness = fitness;
            }

            public (TGenotype, float) ToTuple() => (this.Genotype, this.Fitness);
        }
    }
}