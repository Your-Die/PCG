using System;
using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    [Serializable]
    public class NeighborhoodProvider
    {
        [SerializeField] private int radius = 1;

        [OdinSerialize, DefaultAsset("Full")] 
        private INeighborhoodFunction function;

        public NeighborhoodProvider()
        {
        }

        public NeighborhoodProvider(INeighborhoodFunction function, int radius = 1)
        {
            this.function = function;
            this.radius = radius;
        }

        public IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, Grid2D grid)
        {
            return this.function.GetNeighborhood(center, this.radius, grid);
        }
    }
}