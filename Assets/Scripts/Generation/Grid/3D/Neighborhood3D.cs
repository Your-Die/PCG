using System.Collections.Generic;
using Generation.Grid;

namespace Chinchillada.Generation
{
    public class Neighborhood3D : INeighborhood
    {
        private readonly Grid3D grid;
        private readonly Coordinate3D center;
        private readonly int radius;
        
        public int Center => grid[this.center];
        public Neighborhood3D(Grid3D grid, Coordinate3D center, int radius)
        {
            this.grid = grid;
            this.center = center;
            this.radius = radius;
        }

        public IEnumerable<int> Horizontal()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> Vertical()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> Orthogonal()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> EastDiagonal()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> WestDiagonal()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> Diagonal()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<int> Full()
        {
            throw new System.NotImplementedException();
        }
    }
}