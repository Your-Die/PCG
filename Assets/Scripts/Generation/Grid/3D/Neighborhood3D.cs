using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Generation.Grid
{
    public class Neighborhood3D : INeighborhood
    {
        private readonly Grid3D grid;
        private readonly Coordinate3D center;
        private readonly int radius;

        public int Center => this.grid[this.center];

        public Neighborhood3D(Grid3D grid, Coordinate3D center, int radius)
        {
            this.grid = grid;
            this.center = center;
            this.radius = radius;
        }

        public IEnumerable<int> Horizontal()
        {
            var minX = this.center.X - this.radius;
            var maxX = this.center.X + this.radius;

            if (minX < 0)
                minX = 0;

            if (maxX >= this.grid.Width)
                maxX = this.grid.Width - 1;

            for (var x = minX; x < this.center.X; x++)
            {
                var coordinate = new Coordinate3D
                {
                    X = x,
                    Y = this.center.Y,
                    Z = this.center.Z
                };
                yield return this.grid[coordinate];
            }

            for (var x = this.center.X + 1; x <= maxX; x++)
            {
                var coordinate = new Coordinate3D
                {
                    X = x,
                    Y = this.center.Y,
                    Z = this.center.Z
                };

                yield return this.grid[coordinate];
            }
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
            {
                var coordinate = new Coordinate3D {X = this.center.X, Y = y, Z = this.center.Z};
                yield return this.grid[coordinate];
            }

            for (var y = this.center.Y + 1; y <= maxY; y++)
            {
                var coordinate = new Coordinate3D {X = this.center.X, Y = y, Z = this.center.Z};
                yield return this.grid[coordinate];
            }
        }

        public IEnumerable<int> Depthical()
        {
            var minZ = this.center.Z - this.radius;
            var maxZ = this.center.Z + this.radius;

            if (minZ < 0)
                minZ = 0;

            if (maxZ >= this.grid.Depth)
                maxZ = this.grid.Depth - 1;

            for (var z = minZ; z < this.center.Z; z++)
            {
                var coordinate = new Coordinate3D {X = this.center.X, Y = this.center.Y, Z = z};
                yield return this.grid[coordinate];
            }

            for (var z = this.center.Z + 1; z <= maxZ; z++)
            {
                var coordinate = new Coordinate3D {X = this.center.X, Y = this.center.Y, Z = z};
                yield return this.grid[coordinate];
            }
        }

        public IEnumerable<int> Orthogonal()
        {
            var horizontal = this.Horizontal();
            var vertical = this.Vertical();
            var depthical = this.Depthical();

            return horizontal.Concat(vertical).Concat(depthical);
        }

        public IEnumerable<int> EastHorizontalDiagonal()
        {
            int minX, maxX, minY, maxY, minZ, maxZ;

            var diagonalRadius = this.radius;

            do
            {
                (minX, maxX, minY, maxY, minZ, maxZ) = GetNeighborhoodBounds(this.center, diagonalRadius, this.grid);
            } while (!IsCube(minX, maxX, minY, maxY, minZ, maxZ, this.center) && --diagonalRadius > 0);

            if (diagonalRadius == 0)
                yield break;

            for (int x = maxX, y = minY; x >= minX && y <= maxY; x--, y++)
            {
                var coordinate = new Coordinate3D {X = x, Y = y, Z = this.center.Z};
                yield return this.grid[coordinate];
            }
        }

        public IEnumerable<int> WestHorizontalDiagonal()
        {
            var (minX, maxX, minY, maxY, _, _) = GetNeighborhoodBounds(this.center, this.radius, this.grid);

            var min = Math.Max(minX, minY);
            var max = Math.Min(maxX, maxY);

            var mid = this.center.X;

            for (var i = min; i < mid; i++)
            {
                var coordinate = new Coordinate3D {X = i, Y = i, Z = this.center.Z};
                yield return this.grid[coordinate];
            }

            for (var i = mid + 1; i <= max; i++)
            {
                var coordinate = new Coordinate3D {X = i, Y = i, Z = this.center.Z};
                yield return this.grid[coordinate];
            }
        }

        public IEnumerable<int> HorizontalDiagonal()
        {
            var westHorizontalDiagonal = this.WestHorizontalDiagonal();
            var eastHorizontalDiagonal = this.EastHorizontalDiagonal();

            return eastHorizontalDiagonal.Concat(westHorizontalDiagonal);
        }

        public IEnumerable<int> EastDepthicalDiagonal()
        {
            int minX, maxX, minY, maxY, minZ, maxZ;

            var diagonalRadius = this.radius;

            do
            {
                (minX, maxX, minY, maxY, minZ, maxZ) = GetNeighborhoodBounds(this.center, diagonalRadius, this.grid);
            } while (!IsCube(minX, maxX, minY, maxY, minZ, maxZ, this.center) && --diagonalRadius > 0);

            if (diagonalRadius == 0)
                yield break;

            for (int z = maxZ, y = minY; z >= minZ && y <= maxY; z--, y++)
            {
                var coordinate = new Coordinate3D {X = this.center.X, Y = y, Z = z};
                yield return this.grid[coordinate];
            }
        }
        
        public IEnumerable<int> WestDepthicalDiagonal()
        {
            var (minX, maxX, minY, maxY, _, _) = GetNeighborhoodBounds(this.center, this.radius, this.grid);

            var min = Math.Max(minX, minY);
            var max = Math.Min(maxX, maxY);

            var mid = this.center.X;

            for (var i = min; i < mid; i++)
            {
                var coordinate = new Coordinate3D {X = this.center.X, Y = i, Z = i};
                yield return this.grid[coordinate];
            }

            for (var i = mid + 1; i <= max; i++)
            {
                var coordinate = new Coordinate3D {X = this.center.X, Y = i, Z = i};
                yield return this.grid[coordinate];
            }
        }

        public IEnumerable<int> DepthicalDiagonal()
        {
            var westHorizontalDiagonal = this.WestDepthicalDiagonal();
            var eastDepthicalDiagonal = this.EastDepthicalDiagonal();
            
            return eastDepthicalDiagonal.Concat(westHorizontalDiagonal);
        }

        public IEnumerable<int> Diagonal()
        {
            var depthicalDiagonal = this.DepthicalDiagonal();
            var horizontalDiagonal = this.HorizontalDiagonal();
            
            return horizontalDiagonal.Concat(depthicalDiagonal);
        }

        public IEnumerable<int> Full()
        {
            var center = this.center;
            var (minX, maxX, minY, maxY, minZ, maxZ) = GetNeighborhoodBounds(center, this.radius, this.grid);
            for (var x = minX; x < center.X; x++)
            for (var y = minY; y < center.Y; y++)
            for (var z = minZ; z < center.Z; z++)
            {
                var coordinate = new Coordinate3D {X = x, Y = y, Z = z};
                yield return this.grid[coordinate];
            }
            
            for (var x = center.X; x <= maxX; x++)
            for (var y = center.Y; y <= maxY;  y++)
            for (var z = center.Z; z <= maxZ; z++)
            {
                var coordinate = new Coordinate3D {X = x, Y = y, Z = z};
                yield return this.grid[coordinate];
            }
        }

        private static (int minX, int maxX, int minY, int maxY, int minZ, int maxZ) GetNeighborhoodBounds(
            Coordinate3D center,
            int radius,
            Grid3D grid)
        {
            var minX = center.X - radius;
            var minY = center.Y - radius;
            var minZ = center.Z = radius;

            var maxX = center.X + radius;
            var maxY = center.Y + radius;
            var maxZ = center.Z + radius;

            if (minX < 0)
                minX = 0;
            if (minY < 0)
                minY = 0;
            if (minZ < 0)
                minZ = 0;

            if (maxX >= grid.Width)
                maxX = grid.Width - 1;
            if (maxY >= grid.Height)
                maxY = grid.Height - 1;
            if (maxZ >= grid.Depth)
                maxZ = grid.Depth - 1;

            return (minX, maxX, minY, maxY, minZ, maxZ);
        }


        /// <summary>
        /// Check if the rectangle defined by the parameters is a square or not.
        /// </summary>
        /// <param name="minX">Left edge.</param>
        /// <param name="maxX">Right edge.</param>
        /// <param name="minY">Top edge.</param>
        /// <param name="maxY">Bottom edge.</param>
        /// <param name="center">The center.</param>
        private static bool IsCube(int minX, int maxX, int minY, int maxY, int minZ, int maxZ, Coordinate3D center)
        {
            var radius = center.X - minX;

            return maxX - center.X == radius &&
                   center.Y - minY == radius &&
                   maxY - center.Y == radius &&
                   center.Z - minZ == radius &&
                   maxZ - center.Z == radius;
        }
    }
}