using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation
{
    public abstract class AsyncGeneratorComponentBase<T> : ChinchilladaBehaviour, IAsyncGenerator<T>
    {
        [SerializeField] private float asyncUpdate = 0.01f;

        [SerializeField] private bool invokeEventAsync;

        [SerializeField] private IRNG random = new UnityRandom();
        
        private IEnumerator routine;

        public T Result { get; private set; }

        public IRNG Random
        {
            get => this.random;
            set => this.random = value;
        }

        public event Action<T> Generated;

        [Button]
        public T Generate()
        {
            this.GenerateWithEvent();
            return this.Result;
        }

        [Button]
        public void StartGenerateAsync()
        {
            if (this.routine != null) 
                this.StopCoroutine(this.routine);

            this.routine = this.GenerateAsyncRoutine(null);
            this.StartCoroutine(this.routine);
        }

        public IEnumerator GenerateAsyncRoutine(Action<T> callback)
        {
            foreach (var generation in this.GenerateAsync())
            {
                this.Result = generation;
                callback?.Invoke(generation);

                if (this.invokeEventAsync) 
                    this.OnGenerated();

                yield return new WaitForSeconds(this.asyncUpdate);
            }

            this.OnGenerated();
        }

        public abstract IEnumerable<T> GenerateAsync();
        
        protected void OnGenerated() => this.Generated?.Invoke(this.Result);

        protected void OnGenerated(T result)
        {
            this.Result = result;
            this.OnGenerated();
        }

        protected virtual T GenerateInternal() => this.GenerateAsync().Last();

        private void GenerateWithEvent()
        {
            this.Result = this.GenerateInternal();
            this.Generated?.Invoke(this.Result);
        }

        T ISource<T>.Get() => this.Generate();
    }
}