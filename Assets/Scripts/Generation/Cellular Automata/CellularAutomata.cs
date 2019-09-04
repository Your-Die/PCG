using System.Linq;
using Chinchillada.Generation.Grid;
using Generation.Grid;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Performs cellular automata operations on <see cref="Grid2D"/>.
    /// </summary>
    public class CellularAutomata : ICellularAutomata
    {
        /// <summary>
        /// The settings for the operations.
        /// </summary>
        private readonly Settings settings;

        /// <summary>
        /// The function that defines the neighborhood for any given cell.
        /// </summary>
        private readonly NeighborFunction neighborSelector;

        /// <summary>
        /// Construct a new instance of <see cref="CellularAutomata"/>.
        /// </summary>
        /// <param name="settings">The settings for the operations.</param>
        /// <param name="neighborhoodProvider">The function that defines the neighborhood for any given cell.</param>
        public CellularAutomata(Settings settings, NeighborFunction neighborSelector)
        {
            this.settings = settings;
            this.neighborSelector = neighborSelector;
        }

        /// <inheritdoc cref="ICellularAutomata"/>
        public IGrid Step(IGrid grid)
        {
            return grid.SelectNeighborhood(this.settings.Radius, this.ApplyRules);
        }

        private int ApplyRules(INeighborhood neighborhood)
        {
            var neighbors = this.neighborSelector.SelectNeighbors(neighborhood);
            var neighborCount = neighbors.Sum();

            if (neighborCount < this.settings.UnderPopulation)
                return 0;

            if (neighborCount > this.settings.OverPopulation)
                return 0;

            if (neighborCount >= this.settings.Reproduction)
                return 1;

            return neighborhood.Center;
        }
    }
}