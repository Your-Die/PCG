using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Generation
{
    public abstract class GeneratorBase<T> : ChinchilladaBehaviour, IGenerator<T>
    {
        [SerializeField] private float asyncUpdate = 0.01f;
        
        public T Result { get; private set; }
        
        public event Action<T> Generated;

        [Button]
        public T Generate()
        {
            this.GenerateWithEvent();
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

        public void GenerateWithEvent()
        {
            this.Result = this.GenerateInternal();
            this.Generated?.Invoke(this.Result);
        }
    }
}