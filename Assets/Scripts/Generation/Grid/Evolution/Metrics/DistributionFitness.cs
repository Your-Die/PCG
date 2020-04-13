using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Generation.Grid;
using Chinchillada.Utilities;
using JetBrains.Annotations;

namespace Chinchillada.Generation.Evolution.Grid
{
    [UsedImplicitly]
    public class DistributionFitness : IMetricEvaluator<IntGrid2D>
    {
        public float Evaluate(IntGrid2D grid)
        {
            var dictionary = new DefaultDictionary<int, int>(0);
            
            for (var x = 0; x < grid.Width; x++)
            for (var y = 0; y < grid.Height; y++)
            {
                var value = grid[x, y];
                var sameNeighbors = CountingRule.CountNeighborhood(x, y, grid, value, 1);

                dictionary[value] += 1 + sameNeighbors;
            }

            var sum = dictionary.Values.Sum();
            var amount = dictionary.Keys.Count;

            return (float)Math.Pow(sum, amount);
        }
    }
}