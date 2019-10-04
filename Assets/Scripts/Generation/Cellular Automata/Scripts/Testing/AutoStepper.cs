using System;
using Chinchillada;
using Chinchillada.Generation.CellularAutomata;
using Chinchillada.Generation.Grid;
using Chinchillada.Timers;
using Chinchillada.Utilities;
using UnityEngine;

namespace Generation.CellularAutomata
{
    public class AutoStepper : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent] private CellularAutomataGenerator generator;

        [SerializeField] private Timer updateTimer = new Timer(0.05f);

        private IGrid grid;
        
        private void Update()
        {
            if (this.updateTimer.IsRunning)
                return;

            if (this.generator.HasGrid)
                this.generator.Step();
            else
                this.generator.GenerateGrid();

        }
    }
}