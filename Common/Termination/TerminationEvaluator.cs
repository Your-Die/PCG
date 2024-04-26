namespace Chinchillada.PCG
{
    using System;
    using UnityEngine;

    [Serializable]
    public class TerminationEvaluator<T> : ITerminationEvaluator<T>
    {
        [SerializeReference] private ITerminationEvaluator terminationEvaluator;
        
        public void Reset() => this.terminationEvaluator.Reset();

        public bool Evaluate(T _) => this.terminationEvaluator.Evaluate();
    }
}