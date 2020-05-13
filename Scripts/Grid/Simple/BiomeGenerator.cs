using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = Chinchillada.Utilities.Random;

namespace Chinchillada.Generation.Grid
{
    public class BiomeGenerator : GeneratorBase<Grid2D>
    {
        [SerializeField] private int iterations;

        [SerializeField, FindComponent] private IGenerator<Grid2D> gridGenerator;

        private Grid2D grid;

        protected override Grid2D GenerateInternal()
        {
            this.GenerateGrid();

            for (var i = 0; i < this.iterations; i++)
                this.Zoom();

            return this.grid;
        }

        public override IEnumerable<Grid2D> GenerateAsync()
        {
            foreach (var result in this.gridGenerator.GenerateAsync())
            {
                this.grid = result;
                yield return this.grid;
            }

            for (var i = 0; i < this.iterations; i++)
            {
                this.Zoom();
                yield return this.grid;
            }
        }

        [Button]
        private void GenerateGrid() => this.grid = this.gridGenerator.Generate();

        [Button]
        private void Zoom()
        {
            var newWidth = this.grid.Width * 2 - 1;
            var newHeight = this.grid.Height * 2 - 1;

            var nextGrid = new Grid2D(newWidth, newHeight);

            // sliding 2x2 window.
            for (var x = 0; x < this.grid.Width - 1; x++)
            for (var y = 0; y < this.grid.Height - 1; y++)
            {
                var topLeft = this.grid[x, y];
                var topRight = this.grid[x + 1, y];
                var bottomLeft = this.grid[x, y + 1];
                var bottomRight = this.grid[x + 1, y + 1];

                // window is used to fill in 3x3 window in next grid.
                var newX = x * 2;
                var newY = y * 2;

                // Copy corners.
                nextGrid[newX, newY] = topLeft;
                nextGrid[newX + 2, newY] = topRight;
                nextGrid[newX, newY + 2] = bottomLeft;
                nextGrid[newX + 2, newY + 2] = bottomRight;

                // Choose sides
                nextGrid[newX + 1, newY] = Random.Choose(topLeft, topRight);
                nextGrid[newX, newY + 1] = Random.Choose(topLeft, bottomLeft);
                nextGrid[newX + 1, newY + 2] = Random.Choose(bottomLeft, bottomRight);
                nextGrid[newX + 2, newY + 1] = Random.Choose(topRight, bottomRight);

                nextGrid[newX + 1, newY + 1] = Random.Choose(topLeft, topRight, bottomLeft, bottomRight);
            }

            this.grid = nextGrid;
        }
    }
}