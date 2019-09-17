using System;
using Chinchillada.Generation.Grid;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Uses <see cref="ICellularAutomata"/> to generate <see cref="Grid2D"/>.
    /// </summary>
    public class CellularAutomataGenerator : ChinchilladaBehaviour, IGenerator<IGrid>
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
        [OdinSerialize] private IGenerator<IGrid> gridGenerator;

        /// <summary>
        /// The current grid.
        /// </summary>
        private IGrid grid;
        
        /// <summary>
        /// Amount of iterations of <see cref="ICellularAutomata"/> to perform.
        /// </summary>
        public int Iterations
        {
            get => this.iterations;
            set => this.iterations = value;
        }

        public bool HasGrid => this.grid != null;

        /// <summary>
        /// Event invoked when a grid is generated.
        /// </summary>
        public event Action<IGrid> GridGenerated;
        
        /// <summary>
        /// Event invoked when a step of <see cref="ICellularAutomata"/> has been performed.
        /// </summary>
        public event Action<IGrid> StepPerformed;

        /// <inheritdoc/>
        public IGrid Generate()
        {
            this.GenerateGrid();
            this.PerformIterations();

            return this.grid;
        }

        /// <summary>
        /// Generate a grid using the <see cref="gridGenerator"/>.
        /// </summary>
        [Button]
        public void GenerateGrid()
        {
            this.grid = this.gridGenerator.Generate();
            this.GridGenerated?.Invoke(this.grid);
        }

        /// <summary>
        /// Perform the <see cref="iterations"/> of <see cref="ICellularAutomata"/> on the <see cref="grid"/>.
        /// </summary>
        [Button]
        public void PerformIterations()
        {
            for (var i = 0; i < this.Iterations; i++)
                this.grid = this.cellularAutomata.Step(this.grid);

            this.StepPerformed?.Invoke(this.grid);
        }

        /// <summary>
        /// Perform a single step of <see cref="ICellularAutomata"/> on the <see cref="grid"/>.
        /// </summary>
        [Button]
        public void Step()
        {
            this.grid = this.cellularAutomata.Step(this.grid);
            this.StepPerformed?.Invoke(this.grid);
        }
    }
}