using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    public class CellularAutomataComponent : SerializedMonoBehaviour, ICellularAutomata
    {
        [OdinSerialize] private NeighborFunction neighborhoodFunction;

        [SerializeField] private Settings settings;

        private CellularAutomata cellularAutomata;
        
        public IGrid Step(IGrid grid) => this.cellularAutomata.Step(grid);

        private  void Awake() => this.ConstructAutomata();

        private void OnValidate() => this.ConstructAutomata();

        private void ConstructAutomata()
        {
            this.cellularAutomata = new CellularAutomata(this.settings, this.neighborhoodFunction);
        }
    }
}