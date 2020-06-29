using System.Collections.Generic;

namespace Chinchillada.Generation.Mazes
{
    public class GridNode : IGraphNode
    {
        public GridNode NorthNeighbor;
        public GridNode EastNeighbor;
        public GridNode SouthNeighbor;
        public GridNode WestNeighbor;

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