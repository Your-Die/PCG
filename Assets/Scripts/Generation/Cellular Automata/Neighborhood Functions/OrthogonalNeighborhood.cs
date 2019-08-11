using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    [CreateAssetMenu]
    public class OrthogonalNeighborhood : NeighhoodFunction
    {
        public override IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid)
        {
            return GetOrthogonalNeighbors(center, radius, grid);
        }

        public static IEnumerable<Coordinate2D> GetOrthogonalNeighbors(Coordinate2D center, int radius, Grid2D grid)
        {
            var horizontal = GetHorizontalNeighbors(center, radius, grid);
            var vertical = GetVerticalNeighbors(center, radius, grid);

            return horizontal.Concat(vertical);
        }
        
        public static IEnumerable<Coordinate2D> GetHorizontalNeighbors(Coordinate2D center, int radius, Grid2D grid)
        {
            var minX = center.X - radius;
            var maxX = center.X + radius;

            if (minX < 0) 
                minX = 0;
            
            if (maxX >= grid.Width) 
                maxX = grid.Width - 1;
            
            for (var x = minX; x < center.X; x++)
                yield return new Coordinate2D {X = x, Y = center.Y};

            for (var x = center.X + 1; x <= maxX; x++)
                yield return new Coordinate2D {X = x, Y = center.Y};
        }

        public static IEnumerable<Coordinate2D> GetVerticalNeighbors(Coordinate2D center, int radius, Grid2D grid)
        {
            var minY = center.Y - radius;
            var maxY = center.Y + radius;

            if (minY < 0) 
                minY = 0;
            
            if (maxY >= grid.Height) 
                maxY = grid.Height - 1;
            
            for (var y = minY; y < center.Y; y++)
                yield return new Coordinate2D {X = center.X, Y = y};

            for (var y = center.Y + 1; y <= maxY; y++)
                yield return new Coordinate2D {X = center.X, Y = y};
        }
    }
}