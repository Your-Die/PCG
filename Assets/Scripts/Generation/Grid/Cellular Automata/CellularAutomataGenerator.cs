using System.Collections.Generic;
using Chinchillada.Utilities;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class CellularAutomataGenerator : GeneratorBase<IntGrid2D>
    {
        [SerializeField] private int iterations;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private IGenerator<IntGrid2D> gridGenerator;

        [SerializeField, FindComponent] private CellularAutomata cellularAutomata;

        private IntGrid2D grid;

        protected override IntGrid2D GenerateInternal()
        {
            this.GenerateGrid();
            this.grid = this.PerformIterations(this.grid, this.iterations);

            return this.grid;
        }

        protected override IEnumerable<IntGrid2D> GenerateAsyncInternal()
        {
            foreach (var result in this.gridGenerator.GenerateAsync())
            {
                this.grid = result;
                yield return this.grid;
            }
            
            for (var i = 0; i < this.iterations; i++)
            {
                this.PerformIteration();
                yield return this.grid;
            }
        }

        [Button]
        public IntGrid2D PerformIterations(IntGrid2D target, int iterationCount)
        {
            this.grid = target;

            for (var i = 0; i < iterationCount; i++)
                this.PerformIteration();
            
            return this.grid;
        }

        private void GenerateGrid() => this.grid = this.gridGenerator.Generate();

        private void PerformIteration() => this.grid = this.cellularAutomata.Step(this.grid);

        [Button]
        private void GenerateGridVerbose()
        {
            this.GenerateGrid();
            this.OnGenerated();
        }

        [Button]
        private void IterateVerbose()
        {
            this.PerformIteration();
            this.OnGenerated();
        }
        
        
        
    }
}