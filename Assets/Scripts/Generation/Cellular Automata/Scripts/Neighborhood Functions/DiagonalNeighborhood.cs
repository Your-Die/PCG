using System.Collections.Generic;
using Chinchillada.Generation.Grid;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// <see cref="NeighborFunction"/> that looks at diagonal neighbors.
    /// </summary>
    [CreateAssetMenu]
    public class DiagonalNeighborhood : NeighborFunction
    {
        public override IEnumerable<int> SelectNeighbors(INeighborhood neighborhood)
        {
            return neighborhood.Diagonal();
        }
    }
}