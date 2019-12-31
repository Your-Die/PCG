using System;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class GridRenderHook : ChinchilladaBehaviour
    {
        [SerializeField] private IObservableGenerator<Grid2D> generator;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private IGridRenderer drawer;

        private void OnEnable() => this.generator.Generated += this.drawer.Render;

        private void OnDisable() => this.generator.Generated -= this.drawer.Render;
    }
}