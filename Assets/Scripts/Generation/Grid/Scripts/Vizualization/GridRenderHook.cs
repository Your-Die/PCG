using System;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class GridRenderHook : ChinchilladaBehaviour
    {
        [SerializeField] private IObservableGenerator<Grid2D> generator;

        [SerializeField] private IGridDrawer drawer;

        private void OnEnable() => this.generator.Generated += this.drawer.Show;

        private void OnDisable() => this.generator.Generated -= this.drawer.Show;
    }
}