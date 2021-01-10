namespace Chinchillada.Generation
{
    using System;
    using UnityEngine;

    [Serializable]
    public class ManualTerminationEvaluator : ITerminationEvaluator
    {
        [SerializeField] private bool isFinished;

        public bool IsFinished
        {
            get => this.isFinished;
            set => this.isFinished = value;
        }

        public void SetFinished() => this.isFinished = true;

        public void Reset() => this.IsFinished = false;

        public bool Evaluate() => this.IsFinished;
    }
}