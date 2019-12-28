using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    public class StringEvolutionComponent : ChinchilladaBehaviour, IGenerator<string>
    {
        [SerializeField] private int generations;

        [SerializeField] private IProvider<string> initialPopulationProvider;

        [SerializeField] private int eliteCount;
        [SerializeField] private int offspringCount;

        [SerializeField] private float mutationChance;

        [SerializeField] private bool stepwise;
        
        private StringEvolution evolution;
        
        [Button]
        public string Generate()
        {
            var initialPopulation = this.initialPopulationProvider.Provide();
            return this.stepwise 
                ? this.GenerateStepwise(initialPopulation) 
                : this.evolution.EvolveGenerations(initialPopulation, this.generations);
        }

        private string GenerateStepwise(IEnumerable<string> initialPopulation)
        {
            int generationCount = 0;
            string fittestIndividual = null;
            
            var evolveStepwise = this.evolution.EvolveGenerationsStepwise(initialPopulation, this.generations);
            foreach (var (individual, fitness) in evolveStepwise)
            {
                fittestIndividual = individual;
                Debug.Log($"Generation {generationCount}: {individual} - {fitness}");
            }

            return fittestIndividual;
        }

        protected override void Awake()
        {
            base.Awake();
            this.evolution = new StringEvolution(
                this.eliteCount, 
                this.offspringCount, 
                this.mutationChance);
        }
    }
}