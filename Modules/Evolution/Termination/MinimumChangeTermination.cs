using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    public class MinimumChangeTermination : ChinchilladaBehaviour, ITerminationEvaluator<IEvolution>
    {
        [SerializeField] private float minimumChange = 0.1f;

        [SerializeField] private int terminationDelay = 10;

        [SerializeField, FindComponent] private IEvolution evolution;
        
        private float previousFittest;

        private int generationsSinceChange;
        
        public bool Evaluate(IEvolution evolutionController)
        {
            var fittest = evolutionController.FittestIndividual.Fitness;
            var change = Mathf.Abs(fittest - this.previousFittest);

            if (change >= this.minimumChange)
            {
                this.previousFittest = fittest;
                this.generationsSinceChange = 0;

                return false;
            }

            this.generationsSinceChange++;
            return this.generationsSinceChange >= this.terminationDelay;
        }

        public void Reset()
        {
            this.generationsSinceChange = 0;
            this.previousFittest = float.MinValue;
        }

        private void OnEnable() => this.evolution.EvolutionStarted += this.Reset;

        private void OnDisable() => this.evolution.EvolutionStarted -= this.Reset;
    }
}