namespace Chinchillada.Generation.CellularAutomata
{
    using UnityEngine;
    
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    
    using Grid;
    
    public class CellularAutomataComponent : SerializedMonoBehaviour, ICellularAutomata
    {
        [SerializeField] private int radius = 1;

        [SerializeField] private CellularConstraints constraints;
        
        [OdinSerialize] private NeighborFunction neighborhoodFunction;

        [SerializeField] private bool inPlace;

        private CellularAutomata cellularAutomata;

        public int Radius => this.radius;
        
        public IGrid Step(IGrid grid) => this.cellularAutomata.Step(grid);
        public int ApplyRules(INeighborhood neighborhood) => this.cellularAutomata.ApplyRules(neighborhood);
        public int CountNeighbors(INeighborhood neighborhood) => this.cellularAutomata.CountNeighbors(neighborhood);

        public void ApplySettings(NeighborFunction function, CellularConstraints constraints, int radius, bool inPlace)
        {
            this.neighborhoodFunction = function;
            this.constraints = constraints;
            this.radius = radius;
            this.inPlace = inPlace;
        }
        
        private  void Awake() => this.ConstructAutomata();

        private void OnValidate() => this.ConstructAutomata();

        private void ConstructAutomata()
        {
            this.cellularAutomata = new CellularAutomata(
                this.constraints, 
                this.radius, 
                this.neighborhoodFunction, 
                this.inPlace);
        }
    }
}