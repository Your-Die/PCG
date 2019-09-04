
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// 3D Implementation of <see cref="ICoordinate"/>.
    /// </summary>
    public class Coordinate3D : Coordinate2D
    {
        /// <summary>
        /// The position on the depth axis.
        /// </summary>
        public int Z { get; set; }

        /// <inheritdoc />
        public override Vector3 ToVector() => new Vector3(this.X, this.Y, this.Z);

        /// <inheritdoc />
        public override string ToString() => $"({this.X}, {this.Y}, {this.Z})";
    }
}