using System.Linq;

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
        private readonly NeighborhoodProvider neighborhoodProvider;

        /// <summary>
        /// Construct a new instance of <see cref="CellularAutomata"/>.
        /// </summary>
        /// <param name="settings">The settings for the operations.</param>
        /// <param name="neighborhoodProvider">The function that defines the neighborhood for any given cell.</param>
        public CellularAutomata(Settings settings, NeighborhoodProvider neighborhoodProvider)
        {
            this.settings = settings;
            this.neighborhoodProvider = neighborhoodProvider;
        }

        /// <inheritdoc cref="ICellularAutomata"/>
        public Grid2D Step(Grid2D grid)
        {
            var nextGrid = new Grid2D(grid.Width, grid.Height);

            this.Step(ref grid, nextGrid);
            
            return grid;
        }

        /// <inheritdoc cref="ICellularAutomata"/>
        public void Step(ref Grid2D grid, Grid2D buffer)
        {
            var coordinates = grid.GetCoordinates().ToList();
            
            foreach (var coordinate in coordinates)
            {
                var value = this.ApplyRules(coordinate, grid);
                coordinate.Set(value, buffer);
            }

            foreach (var coordinate in coordinates)
            {
                var value = coordinate.Get(buffer);
                coordinate.Set(value, grid);
            }
        }

        /// <summary>
        /// Apply the cellular automata rules on the neighborhood of the <paramref name="coordinate"/>
        /// on the <paramref name="grid"/>.
        /// </summary>
        /// <returns>The resulting state of the cell at <paramref name="coordinate"/>.</returns>
        private int ApplyRules(Coordinate2D coordinate, Grid2D grid)
        {
            var neighborhood = this.neighborhoodProvider.GetNeighborhood(coordinate, grid);
            var neighborCount = neighborhood.Sum(neighborCoordinate => neighborCoordinate.Get(grid));

            if (neighborCount < this.settings.UnderPopulation)
                return 0;

            if (neighborCount > this.settings.OverPopulation)
                return 0;

            if (neighborCount >= this.settings.Reproduction)
                return 1;

            return coordinate.Get(grid);
        }
    }
}