using Chinchillada.Generation.Grid;

namespace Chinchillada.Generation.CellularAutomata
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Settings/CA2D")]
    public class CA3DSettings : ScriptableObject
    {
        [Header("Iterations")]
        [SerializeField] private int iterations = 1;

        [SerializeField] private bool inPlace;
    
        [Header("Grid settings")]
        [SerializeField] private Vector3Int dimensions = new Vector3Int(10, 10, 10);

        [SerializeField, Range(0, 1)] private float fillPercentage = 0.5f;
        
        [Header("Neighborhood")]
        [SerializeField] private int neighborhoodRadius = 1;

        [SerializeField] private NeighborFunction neighborFunction;
        
        [SerializeField] private CellularConstraints constraints;

        public void Apply(
            Grid3DGeneratorComponent gridGenerator,
            CellularAutomataGenerator automataGenerator,
            CellularAutomataComponent automata)
        {
            automataGenerator.Iterations = this.iterations;

            gridGenerator.ApplySettings(this.dimensions, this.fillPercentage);

            automata.ApplySettings(this.neighborFunction, this.constraints, this.neighborhoodRadius, this.inPlace);
        }
    }
}