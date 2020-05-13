﻿using Chinchillada.Foundation;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class GenerationRenderHook<T> : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent(SearchStrategy.Anywhere)]
        private IGenerator<T> generator;

        [SerializeField, FindComponent(SearchStrategy.Anywhere)]
        private IRenderer<T> resultRenderer;

        private void OnEnable() => this.generator.Generated += this.OnGenerated;

        private void OnDisable() => this.generator.Generated -= this.OnGenerated;

        private void OnGenerated(T result) => this.resultRenderer.Render(result);
    }
}