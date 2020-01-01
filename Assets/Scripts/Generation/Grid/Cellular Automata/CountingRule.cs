using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    [Serializable]
    public class CountingRule : ICellularRule
    {
        [SerializeField] private string name;
        
        [SerializeField] private int output;

        [Header("Neighborhood")] [SerializeField]
        private int radius = 1;

        [SerializeField] private NeighborhoodType neighborhoodType;

        [SerializeField] private int constraintTarget = 0;

        [SerializeField] private CountConstraint constraint = new CountConstraint();
        
        public int Apply(int x, int y, Grid2D grid)
        {
            var count = this.CountNeighborhood(x, y, grid);
            var shouldApply = this.constraint.ValidateConstraint(count);

            return shouldApply ? this.output : grid[x, y];
        }

        /// <summary>
        /// Counts the amount of neighbors that satisfy the constraint.
        /// </summary>
        private int CountNeighborhood(int x, int y, Grid2D grid)
        {
            // Count amount of target value in the neighborhood.
            IEnumerable<int> neighbors;
            switch (this.neighborhoodType)
            {
                case NeighborhoodType.Orthogonal:
                    neighbors = this.GetOrthogonalNeighbors(x, y, grid);
                    break;
                case NeighborhoodType.Full:
                    neighbors = this.GetAllNeighbors(x, y, grid);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return neighbors.Count(neighbor => neighbor == this.constraintTarget);
        }

        /// <summary>
        /// Get the orthogonally connected neighbors around (<paramref name="x"/>, <paramref name="y"/>)
        /// on the <paramref name="grid"/>.
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        private IEnumerable<int> GetOrthogonalNeighbors(int centerX, int centerY, Grid2D grid)
        {
            var (xMin, xMax, yMin, yMax) = this.CalculateBounds(centerX, centerY, grid);

            for (var x = xMin; x < centerX; x++)
                yield return grid[x, centerY];

            for (var x = centerX + 1; x <= xMax; x++)
                yield return grid[x, centerY];

            for (var y = yMin; y < centerY; y++)
                yield return grid[centerX, y];

            for (var y = centerY + 1; y <= yMax; y++)
                yield return grid[centerX, y];
        }

        /// <summary>
        /// Get all neighbors around (<paramref name="x"/>, <paramref name="y"/>) on the <paramref name="grid"/>.
        /// </summary>
        private IEnumerable<int> GetAllNeighbors(int centerX, int centerY, Grid2D grid)
        {
            var (xMin, xMax, yMin, yMax) = this.CalculateBounds(centerX, centerY, grid);

            for (var x = xMin; x <= xMax; x++)
            for (var y = yMin; y <= yMax; y++)
            {
                if (x == centerX && y == centerY)
                    continue;

                yield return grid[x, y];
            }
        }

        /// <summary>
        /// Calculates the bounds of the neighborhood around <paramref name="x"/> and <paramref name="y"/>
        /// on the <paramref name="grid"/>.
        /// </summary>
        private (int xMin, int xMax, int yMin, int yMax) CalculateBounds(int x, int y, Grid2D grid)
        {
            var xMin = Mathf.Max(x - this.radius, 0);
            var yMin = Mathf.Max(y - this.radius, 0);
            var xMax = Mathf.Min(x + this.radius, grid.Width - 1);
            var yMax = Mathf.Min(y + this.radius, grid.Height - 1);

            return (xMin, xMax, yMin, yMax);
        }

        /// <summary>
        /// Type/Shape of the neighborhood.
        /// </summary>
        private enum NeighborhoodType
        {
            Orthogonal,
            Full
        }
    }
}