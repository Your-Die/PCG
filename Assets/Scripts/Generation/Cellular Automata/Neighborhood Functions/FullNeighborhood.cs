using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// <see cref="NeighhoodFunction"/> that looks at orthogonal and diagonal neighbors.
    /// </summary>
    [CreateAssetMenu]
    public class FullNeighborhood : NeighhoodFunction
    {
        /// <inheritdoc />
        public override IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid)
        {
            var orthogonal = OrthogonalNeighborhood.GetOrthogonalNeighbors(center, radius, grid);
            var diagonal = DiagonalNeighborhood.GetDiagonalNeighbors(center, radius, grid);

            return orthogonal.Concat(diagonal);
        }
    }
}