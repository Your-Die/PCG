using System;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    public class CellularAutomataGenerator : ChinchilladaBehaviour, IGenerator<Grid2D>
    {
        [SerializeField] private int iterations = 4;

        [SerializeField] private CellularAutomataComponent cellularAutomata;

        [SerializeField] private IGenerator<Grid2D> gridGenerator;

        private Grid2D grid;

        private Grid2D buffer;

        public event Action<Grid2D> GridGenerated;
        public event Action<Grid2D> Stepped;

        public Grid2D Generate()
        {
            this.GenerateGrid();
            this.PerformIterations();

            return this.grid;
        }

        [Button]
        private void GenerateGrid()
        {
            this.grid = this.gridGenerator.Generate();
            this.buffer = this.grid.CopyShape();

            this.GridGenerated?.Invoke(this.grid);
        }

        [Button]
        private void PerformIterations()
        {
            for (var i = 0; i < this.iterations; i++)
                this.cellularAutomata.Step(ref this.grid, this.buffer);

            this.Stepped?.Invoke(this.grid);
        }

        [Button]
        private void Step()
        {
            this.cellularAutomata.Step(ref this.grid, this.buffer);
            this.Stepped?.Invoke(this.grid);
        }
    }
}