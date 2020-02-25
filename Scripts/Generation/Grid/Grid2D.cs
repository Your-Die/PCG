using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class Grid2D : IEnumerable<int>
    {
        private int[,] Items { get; }

        public int Width { get; }

        public int Height { get; }

        public int Size => this.Width * this.Height;
        
        public Vector2Int Shape => new Vector2Int(this.Width, this.Height);

        public int this[int x, int y]
        {
            get => this.Items[x, y];
            set => this.Items[x, y] = value;
        }
        
        public int this[Vector2Int position]
        {
            get => this[position.x, position.y];
            set => this[position.x, position.y] = value;
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

        public bool Contains(Vector2Int position)
        {
            return position.x >= 0 && position.x < this.Width &&
                   position.y >= 0 && position.y < this.Height;
        }

        public GridNeighborhood GetRegion(int x, int y, int radius) => new GridNeighborhood(this, x, y, radius);
        public IEnumerator<int> GetEnumerator()
        {
            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
                yield return this[x, y];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}