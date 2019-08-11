using System.Collections.Generic;

namespace Chinchillada.Generation.CellularAutomata
{
    public interface INeighborhoodFunction
    {
        IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid);
    }
}