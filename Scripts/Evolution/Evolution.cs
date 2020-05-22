using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Chinchillada.Generation.Evolution
{
    public class Evolution<T> : GeneratorBase<T>, IEvolution
    {
        #region Editor fields

        /// <summary>
        /// Amount of offspring to generate each generation.
        /// </summary>
        [SerializeField] private int offspringCount;

        /// <summary>
        /// The size of the initial population.
        /// </summary>
        [SerializeField] private int initialPopulationCount = 100;

        [SerializeField] private GoalType goalType = GoalType.Maximize;

        /// <summary>
        /// Generates the initial population.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private IGenerator<T> initialPopulationGenerator;

        /// <summary>
        /// Evaluates the fitness of candidates.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private IMetricEvaluator<T> fitnessEvaluator;

        /// <summary>
        /// Selects parent candidates for generating offspring.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private IParentSelector parentSelector;

        /// <summary>
        /// Generates offspring from parents.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private IOffspringGenerator<T> offspringGenerator;

        /// <summary>
        /// Selects which individuals survive to the next generation.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private ISurvivorSelector survivorSelector;

        /// <summary>
        /// Evaluates when the evolution should terminate.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private ITerminationEvaluator terminationEvaluator;

        #endregion

        private enum GoalType
        {
            Minimize,
            Maximize
        }

        /// <summary>
        /// The current population of genotypes.
        /// </summary>
        private IList<Genotype<T>> population;

        /// <summary>
        /// The fittest individual genotype in the population.
        /// </summary>
        private Genotype<T> fittestIndividual;

        /// <summary>
        /// The fittest individual in the population.
        /// </summary>
        public IGenotype FittestIndividual => this.fittestIndividual;

        /// <summary>
        /// Event invoked when the evolution is started.
        /// </summary>
        public event Action EvolutionStarted;

        public event Action<IEnumerable<Genotype<T>>> InitialPopulationGenerated;

        /// <summary>
        /// Run the evolution.
        /// </summary>
        /// <returns>The fittest individual of the final generation.</returns>
        [Button]
        public T Evolve()
        {
            this.EvolveGenerationWise().Enumerate();
            return this.fittestIndividual.Candidate;
        }

        /// <summary>
        /// Enumerates the evolution process one generation at a time.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of the best individuals of each generation.</returns>
        public IEnumerable<Genotype<T>> EvolveGenerationWise()
        {
            // Call event.
            this.EvolutionStarted?.Invoke();

            // Generate initial population.
            this.GenerateInitialPopulation();
            yield return this.fittestIndividual;

            var stopWatch = new Stopwatch();
            var generation = 1;
            do
            {
                stopWatch.Restart();
                // Evolve a single generation.
                this.EvolveGeneration();
                
                Debug.Log($"generation {generation++}: {stopWatch.Elapsed}");
                
                yield return this.fittestIndividual;
            } while (!this.terminationEvaluator.Evaluate(this));
        }

        /// <summary>
        /// Evolves a generation of individuals.
        /// </summary>
        /// <returns>The evolved generation.</returns>
        [Button]
        public IEnumerable<Genotype<T>> EvolveGeneration()
        {
            // Select parents.
            var parentGenotypes = this.parentSelector.SelectParents(this.population).ToList();
            var parents = parentGenotypes.Select(genotype => ((Genotype<T>) genotype).Candidate);

            // Generate and evaluate offspring.
            var offspringCandidates = this.offspringGenerator.GenerateOffspring(parents, this.offspringCount);
            var offspring = this.EvaluatePopulation(offspringCandidates);

            // Select survivors.
            var survivors = this.survivorSelector.SelectSurvivors(parentGenotypes, offspring);
            this.population = survivors.Convert(survivor => (Genotype<T>) survivor).ToList();

            // Update fittest individual.
            this.UpdateFittestIndividual();
            return this.population;
        }

        /// <summary>
        /// Generate a new population using the <see cref="initialPopulationGenerator"/>.
        /// </summary>
        [Button]
        public void GenerateInitialPopulation()
        {
            var candidates = this.initialPopulationGenerator.Generate(this.initialPopulationCount);
            this.population = this.EvaluatePopulation(candidates);

            this.InitialPopulationGenerated?.Invoke(this.population);
            this.UpdateFittestIndividual();
        }


        /// <inheritdoc/>
        public override IEnumerable<T> GenerateAsync()
        {
            return this.EvolveGenerationWise().Select(fittestGenotype => fittestGenotype.Candidate);
        }

        /// <inheritdoc/>
        protected override T GenerateInternal() => this.Evolve();

        /// <summary>
        /// Evaluate the <paramref name="candidates"/>.
        /// </summary>
        /// <returns>An ordered list of evaluated <see cref="Genotype{T}"/>.</returns>
        private IList<Genotype<T>> EvaluatePopulation(IEnumerable<T> candidates)
        {
            var evaluatedPopulation = candidates.Select(this.EvaluateFitness);
            var sortedPopulation = SortPopulation();

            return sortedPopulation.ToList();

            IEnumerable<Genotype<T>> SortPopulation()
            {
                switch (this.goalType)
                {
                    case GoalType.Minimize:
                        return evaluatedPopulation.OrderBy(GetFitness);
                    case GoalType.Maximize:
                        return evaluatedPopulation.OrderByDescending(GetFitness);

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            float GetFitness(Genotype<T> genotype) => genotype.Fitness;
        }

        /// <summary>
        /// Evaluate the fitness of the <paramref name="candidate"/>.
        /// </summary>
        private Genotype<T> EvaluateFitness(T candidate)
        {
            var fitness = this.fitnessEvaluator.Evaluate(candidate);
            return new Genotype<T>(candidate, fitness);
        }

        /// <summary>
        /// Update the <see cref="fittestIndividual"/>.
        /// </summary>
        private void UpdateFittestIndividual() => this.fittestIndividual = this.population.First();
    }
}