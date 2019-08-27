using System;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Settings for <see cref="ICellularAutomata"/>.
    /// </summary>
    [Serializable]
    public class Settings
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

        /// <summary>
        /// How many neighbors is too little?
        /// </summary>
        public int UnderPopulation
        {
            get => this.underPopulation;
            set => this.underPopulation = value;
        }

        /// <summary>
        /// From how many neighbors is enough to reproduce?
        /// </summary>
        public int Reproduction
        {
            get => this.reproduction;
            set => this.reproduction = value;
        }

        /// <summary>
        /// How many neighbors is too little?
        /// </summary>
        public int OverPopulation
        {
            get => this.overPopulation;
            set => this.overPopulation = value;
        }
    }
}