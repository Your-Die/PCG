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
        private readonly CellularConstraints constraints;

        private readonly int radius;

        /// <summary>
        /// The function that defines the neighborhood for any given cell.
        /// </summary>
        private readonly NeighborFunction neighborSelector;

        /// <summary>
        /// Construct a new instance of <see cref="CellularAutomata"/>.
        /// </summary>
        /// <param name="constraints">The settings for the operations.</param>
        /// <param name="neighborSelector">The function that defines the neighborhood for any given cell.</param>
        public CellularAutomata(CellularConstraints constraints, int radius, NeighborFunction neighborSelector)
        {
            this.constraints = constraints;
            this.radius = radius;
            this.neighborSelector = neighborSelector;
        }

        /// <inheritdoc cref="ICellularAutomata"/>
        public IGrid Step(IGrid grid)
        {
            return grid.SelectNeighborhood(this.radius, this.ApplyRules);
        }

        private int ApplyRules(INeighborhood neighborhood)
        {
            var neighbors = this.neighborSelector.SelectNeighbors(neighborhood);
            var neighborCount = neighbors.Sum();

            return this.constraints.Apply(neighborhood.Center, neighborCount);
        }
    }
}