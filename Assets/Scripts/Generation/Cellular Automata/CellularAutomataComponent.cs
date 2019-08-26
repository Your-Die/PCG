using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    public class CellularAutomataComponent : SerializedMonoBehaviour, ICellularAutomata
    {
        [OdinSerialize] private NeighborhoodProvider neighborhoodProvider;

        [SerializeField] private Settings settings;

        private CellularAutomata cellularAutomata;
        
        public Grid2D Step(Grid2D grid) => this.cellularAutomata.Step(grid);

        public void Step(ref Grid2D grid, Grid2D buffer) => this.cellularAutomata.Step(ref grid, buffer);

        private  void Awake() => this.ConstructAutomata();

        private void OnValidate() => this.ConstructAutomata();

        private void ConstructAutomata()
        {
            this.cellularAutomata = new CellularAutomata(this.settings, this.neighborhoodProvider);
        }
    }
}