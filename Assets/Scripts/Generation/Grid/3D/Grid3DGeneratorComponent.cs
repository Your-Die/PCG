using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class Grid3DGeneratorComponent : MonoBehaviour, IGenerator<Grid3D>
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int depth;
        
        [SerializeField, Range(0, 1)] private float fillPercentage = 0.5f;
        
        private Grid3DGenerator generator;

        public Grid3D Generate() => this.generator.Generate();
        
        private void Awake() => this.ConstructGenerator();

        private void OnValidate() => this.ConstructGenerator();

        private void ConstructGenerator()
        {
            this.generator = new Grid3DGenerator(
                this.width, 
                this.height, 
                this.depth, 
                this.fillPercentage);
        }

    }
}