using Chinchillada;
using Chinchillada.Generation;
using Chinchillada.Generation.CellularAutomata;
using Chinchillada.Generation.Grid;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = Chinchillada.Utilities.Random;

namespace Generation.Grid
{
    /// <summary>
    /// Tests the <see cref="CellularAutomataGenerator"/>.
    /// </summary>
    public class CATester : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent, Required]
        private CellularAutomataGenerator generator;

        [SerializeField, FindComponent, Required]
        private GridDrawer gridDrawer; 
        
        [SerializeField] private bool specifySeed;
        
        [SerializeField, ShowIf(nameof(specifySeed))]
        private int seed;
        
        [Button]
        public void Test()
        {
            if (this.specifySeed) 
                Random.SetSeed(this.seed);

            var grid = this.generator.Generate();
            this.ShowGrid(grid);
        }
        
        private void ShowGrid(IGrid grid) => this.gridDrawer.Show(grid);

        private void OnEnable()
        {
            this.generator.GridGenerated += this.ShowGrid;
            this.generator.StepPerformed += this.ShowGrid;
        }

        private void OnDisable()
        {
            this.generator.GridGenerated -= this.ShowGrid;
            this.generator.StepPerformed -= this.ShowGrid;
        }
    }
}