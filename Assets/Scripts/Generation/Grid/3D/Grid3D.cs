using System;
using System.Collections.Generic;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Implementation of <see cref="IGrid"/> in 3D space.
    /// </summary>
    public class Grid3D : IGrid<Coordinate3D>
    {
        /// <summary>
        /// The values that make up the grid.
        /// </summary>
        private int[,,] Items { get; }

        /// <summary>
        /// The size in the horizontal dimension.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The size in the vertical dimension.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// The size in the depth dimension.
        /// </summary>
        public int Depth { get; }

        /// <summary>
        /// Get or set the value on this <see cref="IGrid"/> at the location indexed by the <paramref name="coordinate"/>.
        /// </summary>
        public int this[Coordinate3D coordinate]
        {
            get => this.Items[coordinate.X, coordinate.Y, coordinate.Z];
            set => this.Items[coordinate.X, coordinate.Y, coordinate.Z] = value;
        }

        /// <summary>
        /// Construct an empty grid of the given dimensions.
        /// </summary>
        public Grid3D(int width, int height, int depth)
        {
            this.Width = width;
            this.Height = height;
            this.Depth = depth;

            this.Items = new int[width, height, depth];
        }

        /// <summary>
        /// Construct a grid of the <paramref name="items"/>.
        /// </summary>
        public Grid3D(int[,,] items)
        {
            this.Width = items.GetLength(0);
            this.Height = items.GetLength(1);
            this.Depth = items.GetLength(2);

            this.Items = items;
        }
        
        /// <inheritdoc />
        public void ForEach(Action<ICoordinate, int> action)
        {
            foreach (var coordinate in this.GetCoordinates())
            {
                var value = this[coordinate];
                action.Invoke(coordinate, value);
            }
        }
        
        /// <inheritdoc />
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

        /// <summary>
        /// Get the neighborhood surrounding the <paramref name="coordinate"/>.
        /// </summary>
        /// <param name="coordinate">The center of the neighborhood.</param>
        /// <param name="radius">The radius of the neighborhood.</param>
        private INeighborhood GetNeighborhood(Coordinate3D coordinate, int radius)
        {
            return new Neighborhood3D(this, coordinate, radius);
        }

        /// <summary>
        /// Get an <see cref="IEnumerable{T}"/> of all the coordinates in this <see cref="Grid3D"/>.
        /// </summary>
        private IEnumerable<Coordinate3D> GetCoordinates()
        {
            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
            for (var z = 0; z < this.Depth; z++)
            {
                yield return new Coordinate3D {X = x, Y = y, Z = z};
            }
        }

        /// <summary>
        /// Create a new <see cref="Grid3D"/> with the same dimensions.
        /// </summary>
        private IGrid<Coordinate3D> CopyShape() => new Grid3D(this.Width, this.Height, this.Depth);
    }
}