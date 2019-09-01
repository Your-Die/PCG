using Generation.Grid;
using UnityEngine;

namespace Chinchillada.Generation
{
    public interface ICoordinate
    {
        /// <summary>
        /// Convert this <see cref="Coordinate2D"/> to a <see cref="Vector3"/>.
        /// </summary>
        /// <returns></returns>
        Vector3 ToVector();
    }
}