using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class GenerationRenderHook<T> : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent(SearchStrategy.Anywhere)]
        private IAsyncGenerator<T> generator;

        [SerializeField, FindComponent(SearchStrategy.Anywhere)]
        private IRenderer<T> resultRenderer;

        private void OnEnable() => this.generator.Generated += this.OnGenerated;

        private void OnDisable() => this.generator.Generated -= this.OnGenerated;

        private void OnGenerated(T result) => this.resultRenderer.Render(result);
    }
}