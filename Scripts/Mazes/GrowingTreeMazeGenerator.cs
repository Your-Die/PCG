using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chinchillada.Generation.Mazes
{
    public class GrowingTreeMazeGenerator : GeneratorBase<GridGraph>
    {
        [SerializeField] private int width;

        [SerializeField] private int height;

        [SerializeField] private INodeSelector nodeSelector;
        
        protected override GridGraph GenerateInternal()
        {
            return this.GenerateAsync().Last();
        }

        public override IEnumerable<GridGraph> GenerateAsync()
        {
            var grid = new GridGraph(this.width, this.height);
            var visitLookup = new bool[this.width, this.height];

            var startNode = ChooseRandomNode(grid);
            var frontier = new List<GridNode> {startNode};
            visitLookup[startNode.X, startNode.Y] = true;

            while (frontier.Any())
            {
                var node = this.nodeSelector.SelectNode(frontier);

                if (TryChooseSuccessor(node, grid, visitLookup, out var successor))
                {
                    frontier.Add(successor);
                    visitLookup[successor.X, successor.Y] = true;

                    yield return grid;
                }
                else
                {
                    frontier.Remove(node);
                }
            }
        }

        private static bool TryChooseSuccessor(GridNode node, GridGraph grid, bool[,] visitLookup, out GridNode successor)
        {
            successor = null;
            
            var directions = EnumHelper.RandomValuesDistinct<Direction>();
            foreach (var direction in directions)
            {
                // Check if not already connected.
                if (node.GetNeighbor(direction) != null)
                    continue;

                // Check if there is a node on the grid.
                var neighbor = grid.GetNeighbor(node, direction);
                if (neighbor == null)
                    continue;

                // Check if successor not already visited.
                if (visitLookup[neighbor.X, neighbor.Y])
                    continue;

                if (!grid.TryConnect(node, direction))
                    continue;
                
                successor = node.GetNeighbor(direction);
                return true;
            }

            return false;
        }

        private static GridNode ChooseRandomNode(GridGraph grid)
        {
            var x = Random.Range(0, grid.Width);
            var y = Random.Range(0, grid.Height);

            return grid[x, y];
        }

   
    }
}