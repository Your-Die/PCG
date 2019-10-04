using System;

namespace Chinchillada.Generation.Grid
{
    using UnityEditor;
    using UnityEngine;
    
    using Sirenix.OdinInspector;
    
    using Utilities;
    using CellularAutomata;

    [RequireComponent(typeof(GridGizmoDrawer))]
    public class CellularLabeler : ChinchilladaBehaviour
    {
        [SerializeField] private Vector3 offset;

        [SerializeField] private Color color = Color.yellow;

        [SerializeField, FindComponent, Required]
        private IGridDrawer gridDrawer;

        [SerializeField, FindComponent(SearchStrategy.InChildren), Required]
        private CellularAutomataComponent automata;
        
        private IGrid grid;

        private GUIStyle style;

        private void OnEnable()
        {
            this.RegisterGrid(this.gridDrawer.Grid);
            this.gridDrawer.NewGridRegistered += this.RegisterGrid;
        }

        private void OnDisable()
        {
            this.RegisterGrid(null);
            this.gridDrawer.NewGridRegistered -= this.RegisterGrid;
        }

        protected override void Awake()
        {
            base.Awake();
            this.UpdateStyles();
        }

        private void OnValidate() => this.UpdateStyles();

        private void UpdateStyles()
        {
            if (this.style == null) 
                this.style = new GUIStyle();
            
            this.style.normal.textColor = this.color;
        }

        private void RegisterGrid(IGrid newGrid) => this.grid = newGrid;

        private void OnDrawGizmos()
        {
            if (this.grid == null)
                return;
            
            foreach (var neighborhood in this.grid.GetNeighborhoods(this.automata.Radius))
            {
                var position = this.gridDrawer.CalculatePosition(neighborhood.Center);
                var neighbors = this.automata.CountNeighbors(neighborhood);

                position += this.offset;
                var labelText = neighbors.ToString();
                
                Handles.Label(position, labelText, this.style);
            }
        }
    }
}