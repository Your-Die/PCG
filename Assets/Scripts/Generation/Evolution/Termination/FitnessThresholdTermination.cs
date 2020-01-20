using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable, UsedImplicitly]
    public class FitnessThresholdTermination : ITerminationEvaluator
    {
        [SerializeField] private float fitnessThreshold = 100;
        
        public bool Evaluate(IEvolution evolver) => evolver.FittestIndividual.Fitness >= this.fitnessThreshold;
    }
}