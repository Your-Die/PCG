using System;
using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Provides the neighborhood of a cell in a <see cref="Grid2D"/>.
    /// </summary>
    [Serializable]
    public class NeighborhoodProvider
    {
        /// <summary>
        /// The radius of the neighborhood.
        /// </summary>
        [SerializeField] private int radius = 1;

        /// <summary>
        /// The function to find the neighborhood.
        /// </summary>
        [OdinSerialize, DefaultAsset("Full")] 
        private INeighborhoodFunction function;

        /// <summary>
        /// Construct a new instance of <see cref="NeighborhoodProvider"/>.
        /// </summary>
        public NeighborhoodProvider()
        {
        }

        
        /// <summary>
        /// Construct a new instance of <see cref="NeighborhoodProvider"/>.
        /// </summary>
        /// <param name="function">The function to find the neighborhood.</param>
        /// <param name="radius">The radius of the neighborhood.</param>
        public NeighborhoodProvider(INeighborhoodFunction function, int radius = 1)
        {
            this.function = function;
            this.radius = radius;
        }

        /// <summary>
        /// Get the neighborhood of the <paramref name="center"/> on the <paramref name="grid"/>.
        /// </summary>
        public IEnumerable<Coordinate2D> GetNeighborhood(Coordinate2D center, Grid2D grid)
        {
            return this.function.GetNeighborhood(center, this.radius, grid);
        }
    }
}