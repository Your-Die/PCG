using System;
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
        private IObservableGenerator<Grid2D> generator;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private IGridRenderer drawer;

        private IObservableGenerator<Grid2D> generatorCache;

        [Button]
        public void InvokeGeneration() => this.generator.Generate();

        private void UpdateGenerator()
        {
            if (this.generator == this.generatorCache)
                return;
            
            Unsubscribe(this.generatorCache, this.drawer);
            Subscribe(this.generator, this.drawer);

            this.generatorCache = this.generator;
        }
        
        private static void Subscribe(IObservableGenerator<Grid2D> generator, IGridRenderer gridRenderer)
        {
            generator.Generated += gridRenderer.Render;
        }

        private static void Unsubscribe(IObservableGenerator<Grid2D> generator, IGridRenderer gridRenderer)
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