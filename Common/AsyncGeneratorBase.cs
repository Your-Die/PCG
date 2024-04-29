using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.PCG
{
    [Serializable]
    public abstract class AsyncGeneratorBase<T> : GeneratorBase<T>, IAsyncGenerator<T>
    {
        [SerializeField, FoldoutGroup("Async settings")] private float asyncDelay = 0.02f;

        [SerializeField, FoldoutGroup("Async settings")]
        private bool registerResultEachIteration;

        protected override T GenerateInternal(IRNG random) => this.GenerateAsync(random).Last();

        public abstract IEnumerable<T> GenerateAsync(IRNG random);

        public IEnumerator GenerateAsyncRoutine(IRNG random, Action<T> callback = null)
        {
            T result = default;
            
            foreach (var iteration in this.GenerateAsync(random))
            {
                result = iteration;
                
                if (this.registerResultEachIteration) 
                    this.RegisterResult(result);
                
                yield return new WaitForSeconds(this.asyncDelay);
            }

            // If we register each generation, we already registered the final result.
            // Otherwise, we do it now.
            if (!this.registerResultEachIteration) 
                this.RegisterResult(result);
        }
    }
}