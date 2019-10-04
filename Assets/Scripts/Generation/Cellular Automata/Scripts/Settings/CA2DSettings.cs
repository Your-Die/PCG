using Chinchillada.Generation.Grid;

namespace Chinchillada.Generation.CellularAutomata
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Settings/CA2D")]
    public class CA2DSettings : ScriptableObject
    {
        [Header("Iterations")]
        [SerializeField] private int iterations = 1;

        [SerializeField] private bool inPlace;
    
        [Header("Grid settings")]
        [SerializeField] private Vector2Int dimensions = new Vector2Int(1, 10);

        [SerializeField, Range(0, 1)] private float fillPercentage = 0.5f;
        
        [Header("Neighborhood")]
        [SerializeField] private int neighborhoodRadius = 1;

        [SerializeField] private NeighborFunction neighborFunction;
        
        [SerializeField] private CellularConstraints constraints;

        public void Apply(
            Grid2DGeneratorComponent gridGenerator,
            CellularAutomataGenerator automataGenerator,
            CellularAutomataComponent automata)
        {
            automataGenerator.Iterations = this.iterations;

            gridGenerator.ApplySettings(this.dimensions.x, this.dimensions.y, this.fillPercentage);

            automata.ApplySettings(this.neighborFunction, this.constraints, this.neighborhoodRadius, this.inPlace);
        }
    }
}