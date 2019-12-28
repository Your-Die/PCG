using System;
using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Draws a <see cref="IGrid"/> using <see cref="Gizmos"/>.
    /// </summary>
    public class GridGizmoDrawer : ChinchilladaBehaviour, IGridDrawer
    {
        /// <summary>
        /// Size of a cube.
        /// </summary>
        [SerializeField] private Vector3 cellSize = Vector3.one;
        
        /// <summary>
        /// Spacing between the cubes.
        /// </summary>
        [SerializeField] private float spacing = 1;
        
        /// <summary>
        /// Top-left point to draw the cubes from.
        /// </summary>
        [SerializeField, FindComponent] private Transform topLeft;

        /// <summary>
        /// Colors for the possible states of the grid cells.
        /// </summary>
        [OdinSerialize] private Dictionary<int, Color> colors;

        public event Action<IGrid> NewGridRegistered;

        public IGrid Grid { get; private set; }

        /// <summary>
        /// Draw the <paramref name="grid"/>.
        /// </summary>
        public void Show(IGrid newGrid) => this.SetGrid(newGrid);

        /// <summary>
        /// Hide the current grid.
        /// </summary>
        public void Hide() => this.SetGrid(null);

        private void OnDisable() => this.Hide();
        
        public Vector3 CalculatePosition(ICoordinate coordinate)
        {
            var offset = this.spacing * coordinate.ToVector();
            return this.topLeft.position + offset;
        }
        
        private void OnDrawGizmos()
        {
            this.Grid?.ForEach((coordinate, value) =>
            {
                // Set color.
                Gizmos.color = this.colors[value];

                // Calculate position.
                var position = this.CalculatePosition(coordinate);

                // Draw cell.
                Gizmos.DrawCube(position, this.cellSize);
            });
        }

        private void SetGrid(IGrid newGrid)
        {
            if (newGrid == this.Grid)
                return;

            this.Grid = newGrid;
            this.NewGridRegistered?.Invoke(this.Grid);
        }
    }
}