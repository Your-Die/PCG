namespace Chinchillada.Generation.CellularAutomata
{
    using System.Linq;
    using Grid;

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

        public int ApplyRules(INeighborhood neighborhood)
        {
            var neighborCount = this.CountNeighbors(neighborhood);
            return this.constraints.Apply(neighborhood.CenterValue, neighborCount);
        }

        public int CountNeighbors(INeighborhood neighborhood)
        {
            var neighbors = this.neighborSelector.SelectNeighbors(neighborhood);
            return neighbors.Sum();
        }
    }
}