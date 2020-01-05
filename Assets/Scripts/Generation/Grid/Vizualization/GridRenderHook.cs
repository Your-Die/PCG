using System.Collections;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class GridRenderHook : ChinchilladaBehaviour
    {
        [SerializeField, 
         FindComponent(SearchStrategy.InChildren), 
         OnValueChanged(nameof(UpdateGenerator))]
        private IGenerator<Grid2D> generator;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private IGridRenderer drawer;

        private IGenerator<Grid2D> generatorCache;

        private IEnumerator routine;

        [Button]
        public void InvokeGeneration() => this.generator.Generate();

        [Button]
        public void InvokeGenerationAsync()
        {
            if (this.routine != null) 
                this.StopCoroutine(this.routine);

            this.routine = this.generator.GenerateAsyncRoutine(this.drawer.Render);
            this.StartCoroutine(this.routine);
        }

        [Button]
        public void StartGenerationAsync()
        {
            if (this.routine != null) 
                this.StopCoroutine(this.routine);

            this.routine = this.generator.GenerateAsyncRoutine(this.drawer.Render);
        }

        [Button]
        public void StepAsyncGeneration()
        {
            if (this.routine == null || !this.routine.MoveNext())
                return;

            var grid = this.generator.Result;
            this.drawer.Render(grid);
        }

        private void UpdateGenerator()
        {
            if (this.generator == this.generatorCache)
                return;
            
            Unsubscribe(this.generatorCache, this.drawer);
            Subscribe(this.generator, this.drawer);

            this.generatorCache = this.generator;
        }
        
        private static void Subscribe(IGenerator<Grid2D> generator, IGridRenderer gridRenderer)
        {
            generator.Generated += gridRenderer.Render;
        }

        private static void Unsubscribe(IGenerator<Grid2D> generator, IGridRenderer gridRenderer)
        {
            if (generator != null) 
                generator.Generated -= gridRenderer.Render;
        }

        protected override void Awake()
        {
            base.Awake();
            this.generatorCache = this.generator;
        }

        private void OnEnable() => Subscribe(this.generator, this.drawer);

        private void OnDisable() => Unsubscribe(this.generator, this.drawer);
    }
}