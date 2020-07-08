using System;
using System.Collections.Generic;
using Chinchillada.Foundation;

namespace Chinchillada.Generation.Mazes
{
    public class GridNode : IGraphNode
    {
        public int X { get; }
        
        public int Y { get; }
        
        public GridNode NorthNeighbor;
        public GridNode EastNeighbor;
        public GridNode SouthNeighbor;
        public GridNode WestNeighbor;

        public GridNode(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public GridNode GetNeighbor(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return this.NorthNeighbor;

                case Direction.East:
                    return this.EastNeighbor;
                
                case Direction.South:
                    return this.SouthNeighbor;
                
                case Direction.West:
                    return this.WestNeighbor;   
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public IEnumerable<IGraphNode> Connections
        {
            get
            {
                if (this.NorthNeighbor != null)
                    yield return this.NorthNeighbor;
                
                if (this.EastNeighbor != null)
                    yield return this.EastNeighbor;
                
                if (this.SouthNeighbor != null)
                    yield return this.SouthNeighbor;
                
                if (this.WestNeighbor != null)
                    yield return this.WestNeighbor;
            }
        }
    }
}