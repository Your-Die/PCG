using System.Text;
using Chinchillada.Colors;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    using System;
    using System.Collections.Generic;

    public class Grid2D : IGrid<Coordinate2D>
    {
        private int[,] Items { get; }

        public int Width { get; }

        public int Height { get; }

        public int this[Coordinate2D coordinate]
        {
            get => this.Items[coordinate.X, coordinate.Y];
            set => this.Items[coordinate.X, coordinate.Y] = value;
        }

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

        public void ForEach(Action<ICoordinate, int> action)
        {
            foreach (var coordinate in this.GetCoordinates())
            {
                var value = coordinate.Get(this);
                action.Invoke(coordinate, value);
            }
        }

        public IEnumerable<INeighborhood> GetNeighborhoods(int radius) => Grid.GetNeighborhoods(this, radius);

        public IGrid SelectNeighborhood(int radius, Func<INeighborhood, int> selector, IGrid output = null)
        {
            var outputGrid = output as Grid2D ?? (Grid2D) this.CopyShape();

            foreach (var coordinate in this.GetCoordinates())
            {
                var neighborhood = this.GetNeighborhood(coordinate, radius);
                var value = selector.Invoke(neighborhood);
                coordinate.Set(value, outputGrid);
            }

            return outputGrid;
        }

        public IEnumerable<Coordinate2D> GetCoordinates()
        {
            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
            {
                yield return new Coordinate2D {X = x, Y = y};
            }
        }

        public INeighborhood GetNeighborhood(Coordinate2D coordinate, int radius)
        {
            return new Neighborhood2D(coordinate, this, radius);
        }

        public Grid2D CopyShape() => new Grid2D(this.Width, this.Height);

        public void Print()
        {
            var stringBuilder = new StringBuilder();

            for (var y = 0; y < this.Height; y++)
            {
                for (var x = 0; x < this.Width; x++)
                {
                    var coordinate = new Coordinate2D{X = x, Y = y};
                    var cell = this[coordinate];
                    
                    stringBuilder.Append($"{cell} ");
                }
                
                Debug.Log(stringBuilder.ToString());
                stringBuilder.Clear();
            }
        }
    }
}