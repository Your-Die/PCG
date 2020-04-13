using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class CellularAutomata : ChinchilladaBehaviour
    {
        [SerializeField] private IList<ICellularRule> rules = new List<ICellularRule>();

        private IntGrid2D outputGrid;
        
        public IntGrid2D Step(IntGrid2D grid)
        {
            var output = this.outputGrid == null || this.outputGrid.Shape != grid.Shape
                ? grid.CopyShape()
                : this.outputGrid;
            
            for (var x = 0; x < grid.Width; x++)
            for (var y = 0; y < grid.Height; y++)
                output[x, y] = this.ApplyRules(x, y, grid);

            this.outputGrid = grid;
            return output;
        }

        private int ApplyRules(int x, int y, IntGrid2D grid)
        {
            var value = grid[x, y];

            for (var i = this.rules.Count - 1; i >= 0; i--)
            {
                var rule = this.rules[i];
                var newValue = rule.Apply(x, y, grid);

                if (newValue != value)
                    return newValue;
            }

            return value;
        }
    }
}