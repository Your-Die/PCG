using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    [CreateAssetMenu]
    public class FullNeighborhood : NeighhoodFunction
    {
        public override IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid)
        {
            var orthogonal = OrthogonalNeighborhood.GetOrthogonalNeighbors(center, radius, grid);
            var diagonal = DiagonalNeighborhood.GetDiagonalNeighbors(center, radius, grid);

            return orthogonal.Concat(diagonal);
        }
    }
}