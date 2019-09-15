using System;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Settings for <see cref="ICellularAutomata"/>.
    /// </summary>
    [Serializable]
    public class CellularConstraints
    {
        /// <summary>
        /// How many neighbors is too little?
        /// </summary>
        [SerializeField] private int underPopulation = 3;
        
        /// <summary>
        /// From how many neighbors is enough to reproduce?
        /// </summary>
        [SerializeField] private int reproduction = 4;
        
        /// <summary>
        /// How many neighbors is too much?
        /// </summary>
        [SerializeField] private int overPopulation = 5;

        public int Apply(int value, int neighbors)
        {
            if (neighbors < this.underPopulation || neighbors > this.overPopulation)
                return 0;

            if (neighbors >= this.reproduction)
                return 1;

            return value;
        }
    }
}