using Chinchillada.Utilities;
using DefaultNamespace;
using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class CellularAutomataGenerator : GeneratorBase<Grid2D>
    {
        [SerializeField] private int iterations;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private IGenerator<Grid2D> gridGenerator;

        [SerializeField, FindComponent] private CellularAutomata cellularAutomata;

        private Grid2D grid;

        protected override Grid2D GenerateInternal()
        {
            this.GenerateGrid();
            this.grid = this.PerformIterations(this.grid, this.iterations);

            return this.grid;
        }
       
        [Button]
        public Grid2D PerformIterations(Grid2D target, int iterationCount)
        {
            this.grid = target;

            for (var i = 0; i < iterationCount; i++)
                this.PerformIteration();
            
            this.OnGenerated(this.grid);
            return this.grid;
        }

        [Button]
        public void GenerateGrid()
        {
            this.grid = this.gridGenerator.Generate();
            this.OnGenerated(this.grid);
        }

        [Button]
        private void PerformIteration()
        {
            this.grid = this.cellularAutomata.Step(this.grid);
            this.OnGenerated(this.grid);
        }
    }
}