using System.Collections.Generic;
using Chinchillada.Grid;
using UnityEngine;

namespace Chinchillada.Generation.Mazes
{
    public class MazeToGrid : IterativeGeneratorComponent<Grid2D>
    {
        [SerializeField] private IIterativeGenerator<GridGraph> mazeGenerator;

        [SerializeField] private bool generateMazeAsync;

        [SerializeField] private int wall = 1;

        [SerializeField] private Vector2Int cellSize = new Vector2Int(5, 5);
        [SerializeField] private int wallThickness = 1;

        protected override Grid2D GenerateInternal()
        {
            var maze = this.mazeGenerator.Generate();
            return this.ConvertToGrid(maze);
        }

        public override IEnumerable<Grid2D> GenerateAsync()
        {
            if (!this.generateMazeAsync)
            {
                var maze = this.mazeGenerator.Generate();
                yield return this.ConvertToGrid(maze);
                yield break;
            }

            foreach (var maze in this.mazeGenerator.GenerateAsync())
                yield return this.ConvertToGrid(maze);
        }


        private Grid2D ConvertToGrid(GridGraph maze)
        {
            var grid = this.CreateEmptyGrid(maze.Width, maze.Height);

            for (var x = 0; x < maze.Width; x++)
            for (var y = 0; y < maze.Height; y++)
            {
                var node = maze[x, y];

                var left = x * (this.cellSize.x + this.wallThickness);
                var top = y * (this.cellSize.y + this.wallThickness);

                DrawWalls(node, left, top);
            }

            DrawEast();
            DrawSouth();
  
            return grid;

            void DrawWalls(GridNode node, int left, int top)
            {
                if (node.NorthNeighbor == null)
                    DrawNorth();

                if (node.WestNeighbor == null) 
                    DrawWest();

                void DrawNorth()
                {
                    var right = left + this.wallThickness + this.cellSize.x;

                    for (var x = left; x < right; x++)
                        grid[x, top] = this.wall;
                }

                void DrawWest()
                {
                    var bottom = top + this.wallThickness + this.cellSize.y;

                    for (var y = top; y < bottom; y++)
                        grid[left, y] = this.wall;
                }
            }

            void DrawSouth()
            {
                var south = grid.Height - 1;

                for (var x = 0; x < grid.Width; x++) 
                    grid[x, south] = this.wall;
            }

            void DrawEast()
            {
                var east = grid.Width - 1;
                for (var y = 0; y < grid.Height; y++) 
                    grid[east, y] = this.wall;
            }
        }

        private Grid2D CreateEmptyGrid(int cellsX, int cellsY)
        {
            var wallsX = cellsX + 1;
            var wallsY = cellsY + 1;

            var cellsWidth = cellsX * this.cellSize.x;
            var cellsHeight = cellsY * this.cellSize.y;

            var wallsWidth = wallsX * this.wallThickness;
            var wallsHeight = wallsY * this.wallThickness;

            var width = cellsWidth + wallsWidth;
            var height = cellsHeight + wallsHeight;

            return new Grid2D(width, height);
        }
    }
}