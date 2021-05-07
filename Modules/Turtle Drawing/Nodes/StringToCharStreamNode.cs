namespace Chinchillada.Generation.Turtle
{
    using Turtle;
    using Generators;
    using UnityEngine;

    public class StringToCharStreamNode : GeneratorNode<CharacterStream>
    {
        [SerializeField] [Input] private string input;
        
        protected override CharacterStream GenerateInternal() => new CharacterStream(this.input);

        protected override void UpdateInputs()
        {
            base.UpdateInputs();
            this.UpdateInput(ref this.input, nameof(this.input));
        }
    }
}