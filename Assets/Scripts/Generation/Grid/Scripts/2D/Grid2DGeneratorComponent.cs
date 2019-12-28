using System;
using Chinchillada.Distributions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class Grid2DGeneratorComponent : SerializedMonoBehaviour, IObservableGenerator<Grid2D>
    {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;

        [SerializeField] private IDistribution<int> valueDistribution;

        private IGenerator<Grid2D> generator;
        
        public event Action<Grid2D> Generated;

        public Grid2D Generate()
        {
            var grid =  this.generator.Generate();
            
            this.Generated?.Invoke(grid);
            return grid;
        }

        private void Awake() => this.ConstructGenerator();

        private void OnValidate() => this.ConstructGenerator();

        private void ConstructGenerator()
        {
            this.generator = new Grid2DGenerator(this.width, this.height, this.valueDistribution);
        }

        public void ApplySettings(int width, int height, float fillPercentage)
        {
            this.width = width;
            this.height = height;
            this.valueDistribution = Flip.Binary(fillPercentage);
        }

    }
}