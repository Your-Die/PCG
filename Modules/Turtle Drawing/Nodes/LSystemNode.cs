namespace Chinchillada.Generation.Turtle
{
    using Turtle;
    using Generators;
    using UnityEngine;

    public class LSystemNode : GeneratorNode<CharacterStream>
    {
        [SerializeField] [Input] private CharacterStream input;

        [SerializeField] [Input] private int iterationCount;

        [SerializeField] [Input] private LSystem lSystem;

        protected override CharacterStream GenerateInternal()
        {
            var output = this.lSystem.Rewrite(this.input.Characters, this.iterationCount);
            return new CharacterStream(output);
        }

        protected override void UpdateInputs()
        {
            base.UpdateInputs();
            
            this.UpdateInput(ref this.iterationCount,   nameof(this.iterationCount));
            this.UpdateInput(ref this.input,   nameof(this.input));
            this.UpdateInput(ref this.lSystem, nameof(this.lSystem));
        }
    }
}