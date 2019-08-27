using System;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Uses <see cref="ICellularAutomata"/> to generate <see cref="Grid2D"/>.
    /// </summary>
    public class CellularAutomataGenerator : ChinchilladaBehaviour, IGenerator<Grid2D>
    {
        /// <summary>
        /// Amount of iterations of <see cref="ICellularAutomata"/> to perform.
        /// </summary>
        [SerializeField] private int iterations = 4;

        /// <summary>
        /// The component that performs the <see cref="ICellularAutomata"/>.
        /// </summary>
        [SerializeField] private ICellularAutomata cellularAutomata;

        /// <summary>
        /// Generates an initial grid to perform the <see cref="ICellularAutomata"/> on.
        /// </summary>
        [SerializeField] private IGenerator<Grid2D> gridGenerator;

        /// <summary>
        /// The current grid.
        /// </summary>
        private Grid2D grid;

        /// <summary>
        /// Buffer used to store intermediate results.
        /// </summary>
        private Grid2D buffer;

        /// <summary>
        /// Event invoked when a grid is generated.
        /// </summary>
        public event Action<Grid2D> GridGenerated;
        
        /// <summary>
        /// Event invoked when a step of <see cref="ICellularAutomata"/> has been performed.
        /// </summary>
        public event Action<Grid2D> StepPerformed;

        /// <inheritdoc/>
        public Grid2D Generate()
        {
            this.GenerateGrid();
            this.PerformIterations();

            return this.grid;
        }

        /// <summary>
        /// Generate a grid using the <see cref="gridGenerator"/>.
        /// </summary>
        [Button]
        private void GenerateGrid()
        {
            this.grid = this.gridGenerator.Generate();
            this.buffer = this.grid.CopyShape();

            this.GridGenerated?.Invoke(this.grid);
        }

        /// <summary>
        /// Perform the <see cref="iterations"/> of <see cref="ICellularAutomata"/> on the <see cref="grid"/>.
        /// </summary>
        [Button]
        private void PerformIterations()
        {
            for (var i = 0; i < this.iterations; i++)
                this.cellularAutomata.Step(ref this.grid, this.buffer);

            this.StepPerformed?.Invoke(this.grid);
        }

        /// <summary>
        /// Perform a single step of <see cref="ICellularAutomata"/> on the <see cref="grid"/>.
        /// </summary>
        [Button]
        private void Step()
        {
            this.cellularAutomata.Step(ref this.grid, this.buffer);
            this.StepPerformed?.Invoke(this.grid);
        }
    }
}