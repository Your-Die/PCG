using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chinchillada;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Chinchillada.PCG.Evolution
{
    public class Evolution<T> : AsyncGeneratorBase<T>, IEvolution
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
        private IAsyncGenerator<T> initialPopulationGenerator;

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
        private  ITerminationEvaluator<IEvolution> terminationEvaluator;

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
        /// <param name="random"></param>
        /// <returns>The fittest individual of the final generation.</returns>
        [Button]
        public T Evolve(IRNG random)
        {
            this.EvolveGenerationWise(random).Enumerate();
            return this.fittestIndividual.Candidate;
        }

        /// <summary>
        /// Enumerates the evolution process one generation at a time.
        /// </summary>
        /// <param name="random"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> of the best individuals of each generation.</returns>
        public IEnumerable<Genotype<T>> EvolveGenerationWise(IRNG random)
        {
            // Call event.
            this.EvolutionStarted?.Invoke();

            // Generate initial population.
            this.GenerateInitialPopulation(random);
            yield return this.fittestIndividual;

            this.terminationEvaluator.Reset();

            var stopWatch = new Stopwatch();
            var generation = 1;
            do
            {
                stopWatch.Restart();
                // Evolve a single generation.
                this.EvolveGeneration(random);
                
                Debug.Log($"generation {generation++}: {stopWatch.Elapsed}");
                
                yield return this.fittestIndividual;
            } while (!this.terminationEvaluator.Evaluate(this));
        }

        /// <summary>
        /// Evolves a generation of individuals.
        /// </summary>
        /// <param name="random"></param>
        /// <returns>The evolved generation.</returns>
        [Button]
        public IEnumerable<Genotype<T>> EvolveGeneration(IRNG random)
        {
            // Select parents.
            var parentGenotypes = this.parentSelector.SelectParents(this.population);
            var parents = parentGenotypes.Select(genotype => ((Genotype<T>) genotype).Candidate);

            // Generate and evaluate offspring.
            var offspringCandidates = this.offspringGenerator.GenerateOffspring(parents, this.offspringCount, random);
            var offspring = this.EvaluatePopulation(offspringCandidates);

            // Select survivors.
            var survivors = this.survivorSelector.SelectSurvivors(this.population, offspring);
            this.population = survivors.Convert(survivor => (Genotype<T>) survivor).ToList();

            // Update fittest individual.
            this.UpdateFittestIndividual();
            return this.population;
        }

        /// <summary>
        /// Generate a new population using the <see cref="initialPopulationGenerator"/>.
        /// </summary>
        /// <param name="random"></param>
        [Button]
        public void GenerateInitialPopulation(IRNG random)
        {
            var candidates = this.initialPopulationGenerator.Generate(this.initialPopulationCount, random);
            this.population = this.EvaluatePopulation(candidates);

            this.InitialPopulationGenerated?.Invoke(this.population);
            this.UpdateFittestIndividual();
        }


        /// <param name="random"></param>
        /// <inheritdoc/>
        public override IEnumerable<T> GenerateAsync(IRNG random)
        {
            return this.EvolveGenerationWise(random).Select(fittestGenotype => fittestGenotype.Candidate);
        }

        /// <param name="random"></param>
        /// <inheritdoc/>
        protected override T GenerateInternal(IRNG random) => this.Evolve(random);

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