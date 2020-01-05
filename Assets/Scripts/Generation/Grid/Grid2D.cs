using System;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class Grid2D
    {
        private int[,] Items { get; }

        public int Width { get; }

        public int Height { get; }
        
        public Vector2Int Shape => new Vector2Int(this.Width, this.Height);

        public int this[int x, int y]
        {
            get => this.Items[x, y];
            set => this.Items[x, y] = value;
        }

        public Grid2D(int[,] items)
        {
            this.Width = items.GetLength(0);
            this.Height = items.GetLength(1);

            this.Items = items;
        }

        public Grid2D(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.Items = new int[width, height];
        }

        public Grid2D CopyShape() => new Grid2D(this.Width, this.Height);

        public GridNeighborhood GetRegion(int x, int y, int radius) => new GridNeighborhood(this, x, y, radius);
    }
}