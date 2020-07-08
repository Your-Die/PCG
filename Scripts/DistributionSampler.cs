using System;
using System.Collections;
using System.Collections.Generic;
using Chinchillada.Distributions;
using UnityEngine;

namespace Chinchillada.Generation
{
    [Serializable]
    public class DistributionSampler<T> : IAsyncGenerator<T>
    {
        [SerializeField] private IDistribution<T> distribution;
        
        public T Result { get; private set; }
        public event Action<T> Generated;
        public T Generate()
        {
           this.Result = this.distribution.Sample();
           this.Generated?.Invoke(this.Result);

           return this.Result;
        }

        public IEnumerable<T> GenerateAsync()
        {
            yield return this.Generate();
        }

        public IEnumerator GenerateAsyncRoutine(Action<T> callback = null)
        {
            this.Generate();
            callback.Invoke(this.Result);

            yield break;
        }
    }
}