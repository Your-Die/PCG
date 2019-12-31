using System;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public interface IGridDrawer
    {
        event Action<IGrid> NewGridRegistered;
        IGrid Grid { get; }

        /// <summary>
        /// Top-left point to draw the cubes from.
        /// </summary>
        Transform TopLeft { get; }

        /// <summary>
        /// Spacing between the cubes.
        /// </summary>
        float Spacing { get; }

        /// <summary>
        /// Draw the <paramref name="grid"/>.
        /// </summary>
        void Show(IGrid newGrid);
    }

    public static class GridDrawerExtensions
    {
        public static Vector3 CalculatePosition(this IGridDrawer drawer, ICoordinate coordinate)
        {
            var offset = drawer.Spacing * coordinate.ToVector();
            return drawer.TopLeft.position + offset;
        }
    }
}