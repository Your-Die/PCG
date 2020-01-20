using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Chinchillada;
using Chinchillada.Generation;
using Chinchillada.Generation.Grid;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class GeneratorBase<T> : ChinchilladaBehaviour, IGenerator<T>
    {
        [SerializeField] private float asyncUpdate = 0.01f;
        
        public T Result { get; private set; }
        
        public event Action<T> Generated;

        [Button]
        public T Generate()
        {
            this.Result = this.GenerateInternal();
            
            this.Generated?.Invoke(this.Result);
            return this.Result;
        }

        public IEnumerator GenerateAsyncRoutine(Action<T> callback)
        {
            foreach (var generation in this.GenerateAsync())
            {
                this.Result = generation;
                callback?.Invoke(generation);
                
                yield return new WaitForSeconds(this.asyncUpdate);
            }

            this.OnGenerated();
        }


        public virtual IEnumerable<T> GenerateAsync()
        {
            yield return this.GenerateInternal();
        }

        
        protected void OnGenerated() => this.Generated?.Invoke(this.Result);


        protected abstract T GenerateInternal();
    }
}