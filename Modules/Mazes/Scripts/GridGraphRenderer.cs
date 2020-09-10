using System.Collections.Generic;
using Chinchillada.Foundation;
using UnityEngine;
using Utilities.Pooling;

namespace Chinchillada.Generation.Mazes
{
    public class GridGraphRenderer : ChinchilladaBehaviour
    {
        [SerializeField] private int cellSize = 1;

        [SerializeField] private ObjectPoolReference linePool;

        private readonly List<LineRenderer> lines = new List<LineRenderer>();

        public void Render(GridGraph grid)
        {
            this.Hide();

            for (var x = 0; x < grid.Width; x++)
            for (var y = 0; y < grid.Height; y++)
            {
                var node = grid[x, y];

                if (node.NorthNeighbor == null)
                    this.RenderNorth(x, y);

                if (node.WestNeighbor == null)
                    this.RenderWest(x, y);
            }

            var maxX = grid.Width - 1;
            var maxY = grid.Height - 1;

            for (var x = 0; x < grid.Width; x++)
                this.RenderSouth(x, maxY);

            for (var y = 0; y < grid.Height; y++)
                this.RenderEast(maxX, y);
        }

        public void Hide()
        {
            foreach (var line in this.lines)
                this.linePool.Return(line.gameObject);

            this.lines.Clear();
        }

        private void RenderNorth(int x, int y)
        {
            var lineY = y - this.cellSize / 2f;
            this.RenderHorizontal(x, lineY);
        }

        private void RenderSouth(int x, int y)
        {
            var lineY = y + this.cellSize / 2f;
            this.RenderHorizontal(x, lineY);
        }
        
        private void RenderEast(int x, int y)
        {
            var lineX = x + this.cellSize / 2f;
            this.RenderVertical(lineX, y);
        }

        private void RenderWest(int x, int y)
        {
            var lineX = x - this.cellSize / 2f;
            this.RenderVertical(lineX, y);
        }

        private void RenderHorizontal(int x, float y)
        {
            var offset = this.cellSize / 2f;

            var minX = x - offset;
            var maxX = x + offset;

            var startPoint = new Vector3(minX, y);
            var endPoint = new Vector3(maxX, y);

            this.RenderLine(startPoint, endPoint);
        }
        
        private void RenderVertical(float x, int y)
        {
            var offset = this.cellSize / 2f;

            var minY = y - offset;
            var maxY = y + offset;

            var startPoint = new Vector3(x, minY);
            var endPoint = new Vector3(x, maxY);

            this.RenderLine(startPoint, endPoint);
        }

        private void RenderLine(params Vector3[] positions)
        {
            var line = this.linePool.Instantiate<LineRenderer>(Vector3.zero, this.transform);
            
            line.transform.position = positions.Average();
            line.SetPositions(positions);
            
            this.lines.Add(line);
        }
    }
}