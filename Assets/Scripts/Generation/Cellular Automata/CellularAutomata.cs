using System.Linq;

namespace Chinchillada.Generation.CellularAutomata
{
    public interface ICellularAutomata
    {
        Grid2D Step(Grid2D grid);
        void Step(Grid2D grid, ref Grid2D nextGrid);
    }

    public class CellularAutomata : ICellularAutomata
    {
        private readonly Settings settings;
        private readonly NeighborhoodProvider neighborhoodProvider;

        public CellularAutomata(Settings settings, NeighborhoodProvider neighborhoodProvider)
        {
            this.settings = settings;
            this.neighborhoodProvider = neighborhoodProvider;
        }

        public Grid2D Step(Grid2D grid)
        {
            var nextGrid = new Grid2D(grid.Width, grid.Height);

            this.Step(grid, ref nextGrid);
            
            return nextGrid;
        }

        public void Step(Grid2D grid, ref Grid2D nextGrid)
        {
            var coordinates = grid.GetCoordinates();
            
            foreach (var coordinate in coordinates)
            {
                var value = this.ApplyRules(coordinate, grid);
                coordinate.Set(value, nextGrid);
            }
        }

        private int ApplyRules(Coordinate2D coordinate, Grid2D grid)
        {
            var neighborhood = this.neighborhoodProvider.GetNeighborhood(coordinate, grid);
            var neighborCount = neighborhood.Sum(neighborCoordinate => coordinate.Get(grid));

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