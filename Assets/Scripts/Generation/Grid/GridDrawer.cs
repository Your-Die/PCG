using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class GridDrawer : ChinchilladaBehaviour
    {
        [SerializeField] private Vector3 cellSize = Vector3.one;
        
        [SerializeField] private float spacing = 1;
        
        [SerializeField, FindComponent] private Transform topLeft;

        [OdinSerialize] private Dictionary<int, Color> colors;

        private IGrid grid;
        
        public void Show(IGrid grid) => this.grid = grid;

        public void Hide() => this.grid = null;

        private void OnDisable() => this.Hide();

        private void OnDrawGizmos()
        {
            if (this.grid == null)
                return;

            this.grid.ForEach((coordinate, value) =>
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