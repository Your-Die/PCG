using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// <see cref="NeighhoodFunction"/> that looks at diagonal neighbors.
    /// </summary>
    [CreateAssetMenu]
    public class DiagonalNeighborhood : NeighhoodFunction
    {
        /// <inheritdoc/>
        public override IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid)
        {
            return GetDiagonalNeighbors(center, radius, grid);
        }

        /// <summary>
        /// Get the diagonal neighbors of <paramref name="center"/>  in the <paramref name="radius"/>
        /// on the <paramref name="grid"/>.
        /// </summary>
        public static IEnumerable<Coordinate2D> GetDiagonalNeighbors(Coordinate2D center, int radius, Grid2D grid)
        {
            var eastDiagonal = GetEastDiagonalNeighbors(center, radius, grid);
            var westDiagonal = GetWestDiagonalNeighbors(center, radius, grid);

            return eastDiagonal.Concat(westDiagonal);
        }
        
        /// <summary>
        /// Get the west-diagonal neighbors of <paramref name="center"/>  in the <paramref name="radius"/>
        /// on the <paramref name="grid"/>.
        /// </summary>
        /// <remarks>The west diagonal starts in the north-west and goes to the south-east.</remarks>
        public static IEnumerable<Coordinate2D> GetWestDiagonalNeighbors(Coordinate2D center, int radius, Grid2D grid)
        {
            var (minX, maxX, minY, maxY) = GetNeighborhoodBounds(center, radius, grid);
            
            var min = Math.Max(minX, minY);
            var max = Math.Min(maxX, maxY);

            var mid = center.X;
            
            for (var i = min; i < mid; i++)
                yield return new Coordinate2D {X = i, Y = i};

            for (var i = mid + 1; i <= max; i++)
                yield return new Coordinate2D {X = i, Y = i};
        }

        /// <summary>
        /// Get the east-diagonal neighbors of <paramref name="center"/>  in the <paramref name="radius"/>
        /// on the <paramref name="grid"/>.
        /// </summary>
        /// <remarks>The east diagonal starts in the north-east and goes to the south-west.</remarks>
        public static IEnumerable<Coordinate2D> GetEastDiagonalNeighbors(Coordinate2D center, int radius, Grid2D grid)
        {
            int minX, maxX, minY, maxY;

            do
            {
                (minX, maxX, minY, maxY) = GetNeighborhoodBounds(center, radius, grid);
            } while (!IsSquare(minX, maxX, minY, maxY, center) && --radius > 0);

            if (radius == 0)
                yield break;


            for (int x = maxX, y = minY; x >= minX && y <= maxY; x--, y++)
                yield return new Coordinate2D {X = x, Y = y};
        }

        /// <summary>
        /// Get the bounds of the neighborhood of <paramref name="center"/>  in the <paramref name="radius"/>
        /// on the <paramref name="grid"/>.
        /// </summary>
        private static (int minX, int maxX, int minY, int maxY) GetNeighborhoodBounds(
            Coordinate2D center,
            int radius,
            Grid2D grid)
        {
            var minX = center.X - radius;
            var minY = center.Y - radius;

            var maxX = center.X + radius;
            var maxY = center.Y + radius;

            if (minX < 0) 
                minX = 0;
            if (minY < 0) 
                minY = 0;

            if (maxX >= grid.Width) 
                maxX = grid.Width - 1;
            if (maxY >= grid.Height) 
                maxY = grid.Height - 1;

            return (minX, maxX, minY, maxY);
        }

        /// <summary>
        /// Check if the rectangle defined by the parameters is a square or not.
        /// </summary>
        /// <param name="minX">Left edge.</param>
        /// <param name="maxX">Right edge.</param>
        /// <param name="minY">Top edge.</param>
        /// <param name="maxY">Bottom edge.</param>
        /// <param name="center">The center.</param>
        private static bool IsSquare(int minX, int maxX, int minY, int maxY, Coordinate2D center)
        {
            var radius = center.X - minX;

            return maxX - center.X == radius &&
                   center.Y - minY == radius &&
                   maxY - center.Y == radius;
        }
    }
}