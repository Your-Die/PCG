using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    public class CellularAutomataComponent : ChinchilladaBehaviour, ICellularAutomata
    {
        [SerializeField] private NeighborhoodProvider neighborhoodProvider;

        [SerializeField] private Settings settings;

        private CellularAutomata cellularAutomata;
        
        public Grid2D Step(Grid2D grid) => this.cellularAutomata.Step(grid);

        public void Step(Grid2D grid, ref Grid2D nextGrid) => this.cellularAutomata.Step(grid, ref nextGrid);

        private void Awake()
        {
            this.cellularAutomata = new CellularAutomata(this.settings, this.neighborhoodProvider);
        }
    }
}