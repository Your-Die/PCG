using System;
using Chinchillada.Generation.CellularAutomata;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    [RequireComponent(typeof(GridDrawer))]
    public class GridLabeler : ChinchilladaBehaviour
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
            this.grid.Neighborhoods
        }
    }
}