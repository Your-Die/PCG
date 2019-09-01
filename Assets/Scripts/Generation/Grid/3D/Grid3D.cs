using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Generation.Grid;

namespace Chinchillada.Generation
{
    public class Grid3D : IGrid<Coordinate3D>
    {
        private int[,,] Items { get; }

        public int Width { get; }

        public int Height { get; }

        public int Depth { get; }

        public int this[Coordinate3D coordinate]
        {
            get => this.Items[coordinate.X, coordinate.Y, coordinate.Z];
            set => this.Items[coordinate.X, coordinate.Y, coordinate.Z] = value;
        }

        public Grid3D(int width, int height, int depth)
        {
            this.Width = width;
            this.Height = height;
            this.Depth = depth;

            this.Items = new int[width, height, depth];
        }

        public Grid3D(int[,,] items)
        {
            this.Width = items.GetLength(0);
            this.Height = items.GetLength(1);
            this.Depth = items.GetLength(2);

            this.Items = items;
        }

        public void ForEach(Action<ICoordinate, int> action)
        {
            var output = this.CopyShape();

            foreach (var coordinate in this.GetCoordinates())
            {
                var value = this[coordinate];
                action.Invoke(coordinate, value);
            }
        }

        public IGrid Select(Func<int, int> selector)
        {
            var output = this.CopyShape();

            foreach (var coordinate in this.GetCoordinates())
            {
                var value = this[coordinate];
                output[coordinate] = selector.Invoke(value);
            }

            return output;
        }

        public IGrid SelectNeighborhood(int radius, Func<INeighborhood, int> selector)
        {
            var output = this.CopyShape();

            foreach (var coordinate in this.GetCoordinates())
            {
                var neighborhood = this.GetNeighborhood(coordinate, radius);
                output[coordinate] = selector.Invoke(neighborhood);
            }

            return output;
        }

        public INeighborhood GetNeighborhood(Coordinate3D coordinate, int radius)
        {
            return new Neighborhood3D(this, coordinate, radius);
        }

        public IEnumerable<Coordinate3D> GetCoordinates()
        {
            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
            for (var z = 0; z < this.Depth; z++)
            {
                yield return new Coordinate3D {X = x, Y = y, Z = z};
            }
        }

        IEnumerable<ICoordinate> IGrid.GetCoordinates()
        {
            throw new NotImplementedException();
        }

        public IGrid<Coordinate3D> CopyShape()
        {
            throw new NotImplementedException();
        }

        IGrid IGrid.CopyShape()
        {
            return this.CopyShape();
        }
    }
}