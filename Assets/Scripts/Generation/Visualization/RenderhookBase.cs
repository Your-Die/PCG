using System.Collections;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Generation.Grid
{
    public class RenderhookBase<T> : ChinchilladaBehaviour
    {
        [SerializeField,
         FindComponent(SearchStrategy.InChildren),
         OnValueChanged(nameof(UpdateGenerator))]
        private IGenerator<T> generator;

        [SerializeField, Required, FindComponent]
        private IRenderer<T> render;
        
        private IEnumerator routine;

        private IGenerator<T> generatorCache;

        public UnityEvent BeforeRender;
       

        [Button]
        public void InvokeGeneration() => this.generator.Generate();

        [Button]
        public void InvokeGenerationAsync()
        {
            if (this.routine != null)
                this.StopCoroutine(this.routine);

            this.routine = this.generator.GenerateAsyncRoutine(this.Render);
            this.StartCoroutine(this.routine);
        }

        [Button]
        public void StartGenerationAsync()
        {
            if (this.routine != null)
                this.StopCoroutine(this.routine);

            this.routine = this.generator.GenerateAsyncRoutine(this.Render);
        }

        [Button]
        public void StepAsyncGeneration()
        {
            if (this.routine == null || !this.routine.MoveNext())
                return;

            var grid = this.generator.Result;
            this.Render(grid);
        }

        private void Render(T content)
        {
            this.BeforeRender.Invoke();
            this.render.Render(content);
        }

        private void UpdateGenerator()
        {
            if (this.generator == this.generatorCache)
                return;

            this.Unsubscribe(this.generatorCache);
            this.Subscribe(this.generator);

            this.generatorCache = this.generator;
        }

        private void Subscribe(IGenerator<T> gridGenerator)
        {
            gridGenerator.Generated += this.Render;
        }

        private void Unsubscribe(IGenerator<T> gridGenerator)
        {
            if (gridGenerator != null)
                gridGenerator.Generated -= this.Render;
        }

        protected override void Awake()
        {
            base.Awake();
            this.generatorCache = this.generator;
        }

        private void OnEnable() => this.Subscribe(this.generator);

        private void OnDisable() => this.Unsubscribe(this.generator);
    }
}