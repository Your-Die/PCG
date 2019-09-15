namespace Chinchillada.Generation.Grid
{
    using UnityEditor;
    using UnityEngine;
    
    using Sirenix.OdinInspector;
    
    using Utilities;
    using CellularAutomata;

    [RequireComponent(typeof(GridDrawer))]
    public class CellularLabeler : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent, Required]
        private GridDrawer drawer;

        [SerializeField, FindComponent(SearchStrategy.InChildren), Required]
        private CellularAutomataComponent automata;
        
        private IGrid grid;

        private void OnEnable()
        {
            this.RegisterGrid(this.drawer.Grid);
            this.drawer.NewGridRegistered += this.RegisterGrid;
        }

        private void OnDisable()
        {
            this.RegisterGrid(null);
            this.drawer.NewGridRegistered -= this.RegisterGrid;
        }
        
        private void RegisterGrid(IGrid newGrid) => this.grid = newGrid;

        private void OnDrawGizmos()
        {
            foreach (var neighborhood in this.grid.GetNeighborhoods(this.automata.Radius))
            {
                var position = this.drawer.CalculatePosition(neighborhood.Center);
                var neighbors = this.automata.CountNeighbors(neighborhood);
                
                Handles.Label(position, neighbors.ToString());
            }
        }
    }
}