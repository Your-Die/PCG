using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    public abstract class NeighhoodFunction : ScriptableObject, INeighborhoodFunction
    {
        public abstract IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, int radius, Grid2D grid);
    }
}