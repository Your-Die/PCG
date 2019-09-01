using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Generation;

namespace Generation.Grid
{
    public class Neighborhood2D : INeighborhood
    {
        private readonly Coordinate2D center;
        private readonly Grid2D grid;
        private readonly int radius;

        public Neighborhood2D(Coordinate2D center, Grid2D grid, int radius)
        {
            this.center = center;
            this.grid = grid;
            this.radius = radius;
        }

        public int Center => this.center.Get(this.grid);

        public IEnumerable<int> Horizontal()
        {
            var minX = this.center.X - this.radius;
            var maxX = this.center.X + this.radius;

            if (minX < 0) 
                minX = 0;
            
            if (maxX >= this.grid.Width) 
                maxX = this.grid.Width - 1;
            
            for (var x = minX; x < this.center.X; x++)
                yield return new Coordinate2D {X = x, Y = this.center.Y}.Get(this.grid);

            for (var x = this.center.X + 1; x <= maxX; x++)
                yield return new Coordinate2D {X = x, Y = this.center.Y}.Get(this.grid);
        }

        public IEnumerable<int> Vertical()
        {
            var minY = this.center.Y - this.radius;
            var maxY = this.center.Y + this.radius;

            if (minY < 0) 
                minY = 0;
            
            if (maxY >= this.grid.Height) 
                maxY = this.grid.Height - 1;
            
            for (var y = minY; y < this.center.Y; y++)
                yield return new Coordinate2D {X = this.center.X, Y = y}.Get(this.grid);

            for (var y = this.center.Y + 1; y <= maxY; y++)
                yield return new Coordinate2D {X = this.center.X, Y = y}.Get(this.grid);
        }

        public IEnumerable<int> Orthogonal()
        {
            var horizontal = this.Horizontal();
            var vertical = this.Vertical();

            return horizontal.Concat(vertical);
        }

        public IEnumerable<int> EastDiagonal()
        {
            int minX, maxX, minY, maxY;

            var diagonalRadius = this.radius;

            do
            {
                (minX, maxX, minY, maxY) = GetNeighborhoodBounds(this.center, diagonalRadius, this.grid);
            } while (!IsSquare(minX, maxX, minY, maxY, this.center) && --diagonalRadius > 0);

            if (diagonalRadius == 0)
                yield break;


            for (int x = maxX, y = minY; x >= minX && y <= maxY; x--, y++)
                yield return new Coordinate2D {X = x, Y = y}.Get(this.grid);
        }

        public IEnumerable<int> WestDiagonal()
        {
            var (minX, maxX, minY, maxY) = GetNeighborhoodBounds(this.center, this.radius, this.grid);
            
            var min = Math.Max(minX, minY);
            var max = Math.Min(maxX, maxY);

            var mid = this.center.X;

            for (var i = min; i < mid; i++)
            {
                var coordinate = new Coordinate2D {X = i, Y = i};
                yield return coordinate.Get(this.grid);
            }

            for (var i = mid + 1; i <= max; i++)
            {
                var coordinate =  new Coordinate2D {X = i, Y = i};
                yield return coordinate.Get(this.grid);
            }
        }

        public IEnumerable<int> Diagonal()
        {
            var eastDiagonal = this.EastDiagonal();
            var westDiagonal = this.WestDiagonal();

            return eastDiagonal.Concat(westDiagonal);
        }

        public IEnumerable<int> Full()
        {
            var orthogonal = this.Orthogonal();
            var Diagonal = this.Diagonal();

            return orthogonal.Concat(Diagonal);
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