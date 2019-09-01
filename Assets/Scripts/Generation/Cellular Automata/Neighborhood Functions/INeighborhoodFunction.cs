using System.Collections.Generic;
using Generation.Grid;

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
        IEnumerable<int> SelectNeighbors(INeighborhood neighborhood);
    }
}