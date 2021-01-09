using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable, UsedImplicitly]
    public class FitnessThresholdTermination :  ITerminationEvaluator<IEvolution>
    {
        [SerializeField] private float fitnessThreshold = 100;

        public void Reset()
        {
        }

        public bool Evaluate(IEvolution evolver) => evolver.FittestIndividual.Fitness >= this.fitnessThreshold;
    }
}