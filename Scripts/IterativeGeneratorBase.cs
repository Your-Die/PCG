using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation
{
    [Serializable]
    public abstract class IterativeGeneratorBase<T> : GeneratorBase<T>, IIterativeGenerator<T>
    {
        [SerializeField, FoldoutGroup("Async settings")] private float asyncDelay = 0.02f;

        [SerializeField, FoldoutGroup("Async settings")]
        private bool registerResultEachIteration;
        
        protected override T GenerateInternal() => this.GenerateAsync().Last();

        public abstract IEnumerable<T> GenerateAsync();

        public IEnumerator GenerateAsyncRoutine(Action<T> callback = null)
        {
            T result = default;
            
            foreach (var iteration in this.GenerateAsync())
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