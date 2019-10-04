using System;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public interface IGridDrawer
    {
        event Action<IGrid> NewGridRegistered;
        IGrid Grid { get; }

        /// <summary>
        /// Draw the <paramref name="grid"/>.
        /// </summary>
        void Show(IGrid newGrid);

        /// <summary>
        /// Hide the current grid.
        /// </summary>
        void Hide();

        Vector3 CalculatePosition(ICoordinate neighborhoodCenter);
    }
 }