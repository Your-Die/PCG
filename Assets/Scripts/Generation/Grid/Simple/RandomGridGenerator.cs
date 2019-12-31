using System;
using Chinchillada.Distributions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class RandomGridGenerator : SerializedMonoBehaviour, IObservableGenerator<Grid2D>
    {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;

        [SerializeField] private IDistribution<int> valueDistribution;

        public event Action<Grid2D> Generated;

        [Button]
        public Grid2D Generate()
        {
            var grid = GenerateGrid(this.width, this.height, this.valueDistribution);

            this.Generated?.Invoke(grid);
            return grid;
        }

        public static Grid2D GenerateGrid(int width, int height, IDistribution<int> valueDistribution)
        {
            var items = new int[width, height];

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                items[x, y] = valueDistribution.Sample();
            
            return new Grid2D(items);
        }
    }
}