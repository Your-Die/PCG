using System;
using System.Linq;
using Chinchillada.Generation.Grid;
using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada.Generation.Evolution.Grid
{
    [Serializable, UsedImplicitly]
    public class CountFitness : IMetricEvaluator<IntGrid2D>
    {
        [SerializeField] private int targetType;

        public float Evaluate(IntGrid2D grid)
        {
            var targetCount = grid.Count(item => item == this.targetType);
            return (float) targetCount / grid.Size;
        }
    }
}