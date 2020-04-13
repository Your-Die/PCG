using System;
using System.Collections.Generic;
using Chinchillada.Distributions;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class RandomGridGenerator : GeneratorBase<IntGrid2D>
    {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;

        [SerializeField] private IDistribution<int> valueDistribution;

        public int Width
        {
            get => this.width;
            set => this.width = value;
        }

        public int Height
        {
            get => this.height;
            set => this.height = value;
        }

        public IDistribution<int> ValueDistribution
        {
            get => this.valueDistribution;
            set => this.valueDistribution = value;
        }

        protected override IntGrid2D GenerateInternal()
        {
            return GenerateGrid(this.Width, this.Height, this.ValueDistribution);
        }

        public static IntGrid2D GenerateGrid(int width, int height, IDistribution<int> valueDistribution)
        {
            var items = new int[width, height];

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                items[x, y] = valueDistribution.Sample();
            
            return new IntGrid2D(items);
        }
    }
}    