using System;
using System.Collections.Generic;

namespace Chinchillada.Generation.Mazes
{
    public class GridGraph : IGraph
    {
        private readonly GridNode[,] nodes;

        public int Width { get; }
        public int Height { get; }

        public GridNode this[int x, int y] => this.nodes[x, y];

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
                this.nodes[x, y] = new GridNode(x, y);
        }
        
        public static GridGraph FullyConnected(int width, int height)
        {
            var graph = new GridGraph(width, height);

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                if (x > 0)
                    graph.ConnectWest(x, y);

                if (y > 0)
                    graph.ConnectNorth(x, y);
            }

            return graph;
        }

        public GridNode GetNeighbor(GridNode node, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return node.Y > 0 ? this.nodes[node.X, node.Y - 1] : null;

                case Direction.East:
                    return node.X < this.Width - 1 ? this.nodes[node.X + 1, node.Y] : null;

                case Direction.South:
                    return node.Y < this.Height - 1 ? this.nodes[node.X, node.Y + 1] : null;

                case Direction.West:
                    return node.X > 0 ? this.nodes[node.X - 1, node.Y] : null;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public bool TryConnect(GridNode node, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    if (node.Y == 0)
                        return false;

                    this.ConnectNorth(node.X, node.Y);
                    return true;
                
                case Direction.East:
                    if (node.X == this.Width - 1)
                        return false;


                    this.ConnectEast(node.X, node.Y);
                    return true;

                case Direction.South:
                    if (node.Y == this.Height - 1)
                        return false;

                    this.ConnectSouth(node.X, node.Y);
                    return true;
                
                case Direction.West:
                    if (node.X == 0)
                        return false;

                    this.ConnectWest(node.X, node.Y);
                    return true;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void ConnectNorth(int x, int y)
        {
            var node = this[x, y];
            var neighbor = this[x, y - 1];

            node.NorthNeighbor = neighbor;
            neighbor.SouthNeighbor = node;
        }

        private void ConnectEast(int x, int y)
        {
            var node = this[x, y];
            var neighbor = this[x + 1, y];

            node.EastNeighbor = neighbor;
            neighbor.WestNeighbor = node;
        }

        private void ConnectSouth(int x, int y)
        {
            var node = this[x, y];
            var neighbor = this[x, y + 1];

            node.SouthNeighbor = neighbor;
            neighbor.NorthNeighbor = node;
        }

        private void ConnectWest(int x, int y)
        {
            var node = this[x, y];
            var neighbor = this[x - 1, y];

            node.WestNeighbor = neighbor;
            neighbor.EastNeighbor = node;
        }
    }
}