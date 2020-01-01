using Chinchillada.Generation.Grid;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Evolution.Grid
{
    public class RandomFitness : MonoBehaviour, IFitnessEvaluator<Grid2D>
    {
        [SerializeField] private Vector2 fitnessRange;
        public float EvaluateFitness(Grid2D item)
        {
            return this.fitnessRange.RandomInRange();
        }
    }
}