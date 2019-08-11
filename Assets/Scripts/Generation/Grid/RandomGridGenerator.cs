using System;
using Chinchillada.Distributions;
using Chinchillada.Generation.CellularAutomata;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class RandomGridGenerator : IGenerator<Grid2D>
    {
        private readonly int width;
        private readonly int height;
        private IDistribution<bool> fillDistribution;

        public RandomGridGenerator(int width, int height, float fillPercentage)
            : this(width, height, Flip.Boolean(fillPercentage))
        {
        }

        public RandomGridGenerator(int width, int height, IDistribution<bool> fillDistribution)
        {
            this.width = width;
            this.height = height;

            this.fillDistribution = fillDistribution;
        }

        public Grid2D Generate()
        {
            var items = new int[this.width, this.height];
            
            for (int x = 0; x < this.width; x++)
            for (int y = 0; y < this.height; y++)
                items[x, y] = this.fillDistribution.Sample().AsBinary();
            
            return new Grid2D(items);
        }
    }
}