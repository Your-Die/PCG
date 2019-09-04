using System.Collections.Generic;
using System.Linq;
using Chinchillada.Generation.Grid;
using Generation.Grid;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// <see cref="NeighborFunction"/> that looks at orthogonal neighbors.
    /// </summary>
    [CreateAssetMenu]
    public class OrthogonalNeighborhood : NeighborFunction
    {
        public override IEnumerable<int> SelectNeighbors(INeighborhood neighborhood)
        {
            return neighborhood.Orthogonal();
        }
    }
}