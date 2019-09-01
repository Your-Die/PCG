using System.Collections.Generic;
using System.Linq;
using Generation.Grid;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// <see cref="NeighborFunction"/> that looks at orthogonal and diagonal neighbors.
    /// </summary>
    [CreateAssetMenu]
    public class FullNeighborhood : NeighborFunction
    {
        public override IEnumerable<int> SelectNeighbors(INeighborhood neighborhood)
        {
            return neighborhood.Full();
        }
    }
}