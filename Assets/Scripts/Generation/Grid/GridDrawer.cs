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

        private Grid2D grid;
        
        public void Show(Grid2D grid) => this.grid = grid;

        public void Hide() => this.grid = null;

        private void OnDisable() => this.Hide();

        private void OnDrawGizmos()
        {
            if (this.grid == null)
                return;

            foreach (var coordinate in this.grid.GetCoordinates())
            {
                // Set color.
                var value = coordinate.Get(this.grid);
                Gizmos.color = this.colors[value];
                
                // Calculate position.
                var offset = this.spacing * coordinate.ToVector();
                var position = this.topLeft.position + offset;
                
                // Draw cell.
                Gizmos.DrawCube(position, this.cellSize);
            }
        }
    }
}