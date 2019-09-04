using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Base interface for a coordinate in a <see cref="IGrid"/>.
    /// </summary>
    public interface ICoordinate
    {
        /// <summary>
        /// Convert this <see cref="ICoordinate"/> to a <see cref="Vector3"/>.
        /// </summary>
        /// <returns></returns>
        Vector3 ToVector();
    }
}