using System.Collections.Generic;
using Chinchillada.Generation.Grid;
using Generation.Grid;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Base class for <see cref="ScriptableObject"/> that implement <see cref="INeighborhoodFunction"/>.
    /// </summary>
    public abstract class NeighborFunction : ScriptableObject, INeighborhoodFunction
    {
        public abstract IEnumerable<int> SelectNeighbors(INeighborhood neighborhood);
    }
}