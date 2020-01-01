using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utilities.Algorithms;

namespace Chinchillada.Generation.Evolution
{
    public class Evolution<T> : GeneratorBase<T>, IEvolution
    {
        [SerializeField] private int offspringCount;
        
        [SerializeField] private int eliteCount;

        [SerializeField] private int initialPopulationCount = 100;
        
        [SerializeField, FindComponent, Required] 
        private IGenerator<T> initialPopulationGenerator;

        [SerializeField, FindComponent, Required] 
        private IFitnessEvaluator<T> fitnessEvaluator;

        [SerializeField, FindComponent, Required] 
        private IOffspringGenerator<T> offspringGenerator;

        [SerializeField, FindComponent, Required] 
        private IParentSelector parentSelector;
        
        [SerializeField, FindComponent, Required] 
        private ITerminationEvaluator terminationEvaluator;
        
        private IList<Genotype<T>> population;

        private Genotype<T> fittestIndividual;

        private static readonly GenotypeComparer GenotypeComparer = new GenotypeComparer();

        public IGenotype FittestIndividual => this.fittestIndividual;
    
        public event Action EvolutionStarted;

        [Button]
        public T Evolve()
        {
            this.EvolveGenerationWise().EnumerateFully();
            return this.fittestIndividual.Candidate;
        }
        
        public IEnumerable<Genotype<T>> EvolveGenerationWise()
        {
            this.EvolutionStarted?.Invoke();
            
            this.GenerateInitialPopulation();
            yield return this.fittestIndividual;

            do
            {
                this.population = this.EvolveGeneration().ToList();
                yield return this.fittestIndividual;
                
            } while (!this.terminationEvaluator.Evaluate(this));
        }

        [Button]
        public IEnumerable<Genotype<T>> EvolveGeneration()
        {
            var parentGenotypes = this.parentSelector.SelectParents(this.population);
            var parents = parentGenotypes.Select(genotype => ((Genotype<T>) genotype).Candidate);

            var offspringCandidates = this.offspringGenerator.GenerateOffspring(parents, this.offspringCount);
            var offspring = this.EvaluatePopulation(offspringCandidates);

            var elites = this.population.Take(this.eliteCount);
            
            this.population = MergeSort.Merge<Genotype<T>>(offspring, elites, GenotypeComparer).ToList();
            this.UpdateFittestIndividual();
            
            return this.population;
        }
        
        [Button]
        public void GenerateInitialPopulation()
        {
            var candidates = this.initialPopulationGenerator.Generate(this.initialPopulationCount);
            this.population = this.EvaluatePopulation(candidates);
            
            this.UpdateFittestIndividual();
        }
        
        protected override T GenerateInternal() => this.Evolve();

        private IList<Genotype<T>> EvaluatePopulation(IEnumerable<T> candidates)
        {
            var evaluatedPopulation = candidates.Select(this.EvaluateFitness);
            return evaluatedPopulation.OrderByDescending(genotype => genotype.Fitness).ToList();
        }

        private Genotype<T> EvaluateFitness(T candidate)
        {
            var fitness = this.fitnessEvaluator.EvaluateFitness(candidate);
            return new Genotype<T>(candidate, fitness);
        }

        private void UpdateFittestIndividual() => this.fittestIndividual = this.population.First();
    }
}