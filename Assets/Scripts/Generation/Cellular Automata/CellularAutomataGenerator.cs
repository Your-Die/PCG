using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    public class CellularAutomataGenerator : ChinchilladaBehaviour, IGenerator<Grid2D>
    {
        [SerializeField] private int iterations = 4;
        
        [SerializeField] private CellularAutomataComponent cellularAutomata;

        [SerializeField] private IGenerator<Grid2D> gridGenerator;

        public Grid2D Generate()
        {
            throw new System.NotImplementedException();
        }
    }
}