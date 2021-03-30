using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class GenerationRenderHook<T> : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent(SearchStrategy.InScene)]
        private IGenerator<T> generator;

        [SerializeField, FindComponent(SearchStrategy.InScene)]
        private IVisualizer<T> resultRenderer;

        private void OnEnable() => this.generator.Generated += this.OnGenerated;

        private void OnDisable() => this.generator.Generated -= this.OnGenerated;

        private void OnGenerated(T result) => this.resultRenderer.Visualize(result);
    }
}