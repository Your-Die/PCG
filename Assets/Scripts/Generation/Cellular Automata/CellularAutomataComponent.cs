using System;
using Chinchillada.Generation.Grid;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    public class CellularAutomataComponent : SerializedMonoBehaviour, ICellularAutomata
    {
        [SerializeField] private int radius = 1;

        [SerializeField] private CellularConstraints constraints;
        
        [OdinSerialize] private NeighborFunction neighborhoodFunction;

        private CellularAutomata cellularAutomata;

        public NeighborFunction NeighborhoodFunction => this.neighborhoodFunction;

        public IGrid Step(IGrid grid) => this.cellularAutomata.Step(grid);

        private  void Awake() => this.ConstructAutomata();

        private void OnValidate() => this.ConstructAutomata();

        private void ConstructAutomata()
        {
            this.cellularAutomata = new CellularAutomata(this.constraints, this.radius, this.NeighborhoodFunction);
        }
    }
}