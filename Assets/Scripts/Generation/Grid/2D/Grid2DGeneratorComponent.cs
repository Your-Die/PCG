using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class Grid2DGeneratorComponent : SerializedMonoBehaviour, IGenerator<Grid2D>
    {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;
        [SerializeField, Range(0, 1)] private float fillPercentage = 0.5f;

        private IGenerator<Grid2D> generator;
        
        public Grid2D Generate() => this.generator.Generate();

        private void Awake() => this.ConstructGenerator();

        private void OnValidate() => this.ConstructGenerator();

        private void ConstructGenerator()
        {
            this.generator = new Grid2DGenerator(this.width, this.height, this.fillPercentage);
        }
    }
}