using Chinchillada.Generation;
using Chinchillada.Generation.CellularAutomata;
using Chinchillada.Utilities;
using UnityEngine;

namespace Generation.Grid
{
    public class GridGeneratorComponent : ChinchilladaBehaviour, IGenerator<Grid2D>
    {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;
        [SerializeField, Range(0, 1)] private float fillPercentage = 0.5f;

        private IGenerator<Grid2D> generator;
        
        public Grid2D Generate() => this.generator.Generate();

        protected override void Awake()
        {
            base.Awake();
            this.generator = new RandomGridGenerator(this.width, this.height, this.fillPercentage);
        }
    }
}