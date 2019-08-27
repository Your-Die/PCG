using System.Collections.Generic;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Interface for functions that find neighborhoods on a <see cref="Grid2D"/> for a cell.
    /// </summary>
    public interface INeighborhoodFunction
    {
        /// <summary>
        /// Get the neighborhood on the <paramref name="grid"/> of <paramref name="radius"/>
        /// surrounding <paramref name="center"/>
        /// </summary>
        IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid);
    }
}