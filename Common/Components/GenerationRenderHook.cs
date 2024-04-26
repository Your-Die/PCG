using Chinchillada;
using UnityEngine;

namespace Chinchillada.PCG
{
    public class GenerationRenderHook<T> : AutoRefBehaviour
    {
        [SerializeField, FindComponent(SearchStrategy.InScene)]
        private IObservableGenerator<T> generator;

        [SerializeField, FindComponent(SearchStrategy.InScene)]
        private IVisualizer<T> resultRenderer;

        private void OnEnable() => this.generator.Generated += this.OnGenerated;

        private void OnDisable() => this.generator.Generated -= this.OnGenerated;

        private void OnGenerated(T result) => this.resultRenderer.Visualize(result);
    }
}