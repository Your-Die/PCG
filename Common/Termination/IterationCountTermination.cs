using System;
using Chinchillada;
using UnityEngine;

namespace Chinchillada.Generation
{
    [Serializable]
    public class IterationCountTermination : ITerminationEvaluator
    {
        [SerializeField] private int generationCount;

        private int generations;

        public bool Evaluate()
        {
            return ++this.generations >= this.generationCount;
        }

        public void Reset()
        {
            this.generations = 0;
        }
    }
}