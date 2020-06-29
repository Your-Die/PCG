using System.Collections.Generic;

namespace Chinchillada.Generation.Mazes
{
    public class GridGraph : IGraph
    {
        private readonly GridNode[,] nodes;

        public int Width { get; }
        public int Height { get; }

        public GridNode[,] Grid => this.nodes;

        public IEnumerable<IGraphNode> Nodes
        {
            get
            {
                for (var x = 0; x < this.Width; x++)
                for (var y = 0; y < this.Height; y++)
                {
                    var node = this.nodes[x, y];
                    if (node != null)
                        yield return node;
                }
            }
        }

        public GridGraph(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.nodes = new GridNode[width, height];
            
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                this.nodes[x, y] = new GridNode();
        }

        public static GridGraph FullyConnected(int width, int height)
        {
            var graph = new GridGraph(width, height);

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                if (x > 0)
                    ConnectWest(x, y);

                if (y > 0)
                    ConnectNorth(x, y);
            }

            return graph;

            void ConnectNorth(int x, int y)
            {
                var node = graph.Grid[x, y];
                var neighbor = graph.Grid[x, y - 1];

                node.NorthNeighbor = neighbor;
                neighbor.SouthNeighbor = node;
            }

            void ConnectWest(int x, int y)
            {
                var node = graph.Grid[x, y];
                var neighbor = graph.Grid[x - 1, y];

                node.WestNeighbor = neighbor;
                neighbor.EastNeighbor = node;
            }
        }
    }
}