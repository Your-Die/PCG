using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    [CreateAssetMenu]
    public class DiagonalNeighborhood : NeighhoodFunction
    {
        public override IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid)
        {
            return GetDiagonalNeighbors(center, radius, grid);
        }

        public static IEnumerable<Coordinate2D> GetDiagonalNeighbors(Coordinate2D center, int radius, Grid2D grid)
        {
            var eastDiagonal = GetEastDiagonalNeighbors(center, radius, grid);
            var westDiagonal = GetWestDiagonalNeighbors(center, radius, grid);

            return eastDiagonal.Concat(westDiagonal);
        }

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

        private static bool IsSquare(int minX, int maxX, int minY, int maxY, Coordinate2D center)
        {
            var radius = center.X - minX;

            return maxX - center.X == radius &&
                   center.Y - minY == radius &&
                   maxY - center.Y == radius;
        }
    }
}