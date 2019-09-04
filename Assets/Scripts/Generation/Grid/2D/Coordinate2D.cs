using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Defines a coordinate on a <see cref="Grid2D"/>.
    /// </summary>
    public class Coordinate2D : ICoordinate
    {
        /// <summary>
        /// The horizontal coordinate.
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// The vertical coordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Get the value on the <paramref name="grid"/> at this <see cref="Coordinate2D"/>.
        /// </summary>
        public int Get(Grid2D grid) => grid[this];

        /// <summary>
        /// Set the value on the <paramref name="grid"/> at this <see cref="Coordinate2D"/>.
        /// </summary>
        public void Set(int value, Grid2D grid) => grid[this] = value;

        /// <summary>
        /// Convert this <see cref="Coordinate2D"/> to a <see cref="Vector3"/>.
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 ToVector() => new Vector3(this.X, this.Y);

        public override string ToString() => $"({this.X}, {this.Y})";
    }
}