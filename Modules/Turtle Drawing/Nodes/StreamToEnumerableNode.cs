namespace Text
{
    using System.Collections.Generic;
    using Generators;
    using Chinchillada.Generation.Turtle;
    using UnityEngine;

    public class StreamToEnumerableNode : GeneratorNode<IEnumerable<char>>
    {
        [SerializeField] [Input] private CharacterStream stream;
        
        protected override IEnumerable<char> GenerateInternal() => this.stream.Characters;

        protected override void UpdateInputs()
        {
            base.UpdateInputs();
            this.UpdateInput(ref this.stream, nameof(this.stream));
        }
    }
}