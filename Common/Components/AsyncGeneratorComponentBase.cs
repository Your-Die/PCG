using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Chinchillada.PCG
{
    public abstract class AsyncGeneratorComponentBase<T> : AutoRefBehaviour, IAsyncGenerator<T>
    {
        [SerializeField] private bool invokeEventAsync;

        [SerializeField] private bool useStopWatch;

        private IEnumerator routine;

        public T Result { get; private set; }

        public event Action<T> Generated;

        [Button]
        public T Generate(IRNG random)
        {
            this.GenerateWithEvent(random);
            return this.Result;
        }

        [Button]
        public void StartGenerateAsync(IRNG random)
        {
            if (this.routine != null)
                this.StopCoroutine(this.routine);

            this.routine = this.GenerateAsyncRoutine(random, null);
            this.StartCoroutine(this.routine);
        }

        public IEnumerator GenerateAsyncRoutine(IRNG random, Action<T> callback)
        {
            var stopWatch = new Stopwatch();

            if (this.useStopWatch)
                stopWatch.Start();

            foreach (var generation in this.GenerateAsync(random))
            {
                this.Result = generation;
                callback?.Invoke(generation);

                if (this.useStopWatch)
                    Debug.Log($"async step time: {stopWatch.Elapsed}");

                if (this.invokeEventAsync)
                    this.OnGenerated();

                yield return null;
                stopWatch.Restart();
            }

            this.OnGenerated();
        }

        public abstract IEnumerable<T> GenerateAsync(IRNG random1);

        protected void OnGenerated() => this.Generated?.Invoke(this.Result);

        protected void OnGenerated(T result)
        {
            this.Result = result;
            this.OnGenerated();
        }

        protected virtual T GenerateInternal(IRNG random) => this.GenerateAsync(random).Last();

        private void GenerateWithEvent(IRNG random)
        {
            this.Result = this.GenerateInternal(random);
            this.Generated?.Invoke(this.Result);
        }
    }
}