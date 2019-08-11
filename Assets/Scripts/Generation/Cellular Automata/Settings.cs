using System;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    [Serializable]
    public class Settings
    {
        [SerializeField] private int underPopulation;
        [SerializeField] private int reproduction;
        [SerializeField] private int overPopulation;

        public int UnderPopulation
        {
            get => this.underPopulation;
            set => this.underPopulation = value;
        }

        public int Reproduction
        {
            get => this.reproduction;
            set => this.reproduction = value;
        }

        public int OverPopulation
        {
            get => this.overPopulation;
            set => this.overPopulation = value;
        }
    }
}