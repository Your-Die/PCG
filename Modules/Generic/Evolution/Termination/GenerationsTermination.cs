using System;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable]
    public class GenerationsTermination : ChinchilladaBehaviour, ITerminationEvaluator
    {
        [SerializeField] private int generationCount;

        [SerializeField, FindComponent] private IEvolution evolutionController;

        private int generations;

        public bool Evaluate(IEvolution evolution)
        {
            return ++this.generations >= this.generationCount;
        }

        private void Reset()
        {
            this.generations = 0;
        }

        private void OnEnable() => this.evolutionController.EvolutionStarted += this.Reset;
        private void OnDisable() => this.evolutionController.EvolutionStarted -= this.Reset;
    }
}