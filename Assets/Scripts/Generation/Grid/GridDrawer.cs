using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Draws a <see cref="IGrid"/> using <see cref="Gizmos"/>.
    /// </summary>
    public class GridDrawer : ChinchilladaBehaviour
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

        /// <summary>
        /// The grid we are currently drawing.
        /// </summary>
        private IGrid grid;
        
        /// <summary>
        /// Draw the <paramref name="grid"/>.
        /// </summary>
        public void Show(IGrid grid) => this.grid = grid;

        /// <summary>
        /// Hide the current grid.
        /// </summary>
        public void Hide() => this.grid = null;

        private void OnDisable() => this.Hide();

        private void OnDrawGizmos()
        {
            this.grid?.ForEach((coordinate, value) =>
            {
                // Set color.
                Gizmos.color = this.colors[value];

                // Calculate position.
                var offset = this.spacing * coordinate.ToVector();
                var position = this.topLeft.position + offset;

                // Draw cell.
                Gizmos.DrawCube(position, this.cellSize);
            });
        }
    }
}