namespace Chinchillada.Generation.Turtle
{
    using Generators;
    using UnityEngine;

    public class GenomeGeneratorNode : GeneratorNode<TurtleGenome>
    {
        [SerializeField] [Input] private string pattern;

        [SerializeField] [Input] private int iterations;
        
        protected override TurtleGenome GenerateInternal()
        {
            return new TurtleGenome(this.pattern, this.iterations);
        }

        protected override void UpdateInputs()
        {
            base.UpdateInputs();

            this.UpdateInput(ref this.pattern, nameof(this.pattern));
            this.UpdateInput(ref this.iterations, nameof(this.iterations));
        }
    }
}