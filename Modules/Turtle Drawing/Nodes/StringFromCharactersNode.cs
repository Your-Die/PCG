namespace Text
{
    using System.Text;
    using Generators;
    using UnityEngine;

    public class StringFromCharactersNode : GeneratorNodeWithPreview<string>
    {
        [SerializeField] [Input] private int length;

        [SerializeField] [Input] private char characterInput;
        
        protected override string GenerateInternal()
        {
            var builder = new StringBuilder();

            for (var i = 0; i < this.length; i++)
            {
                var character = this.UpdateInput(ref this.characterInput, nameof(this.characterInput));
                builder.Append(character);
            }

            return builder.ToString();
        }

        protected override void UpdateInputs()
        {
            base.UpdateInputs();

            this.UpdateInput(ref this.length, nameof(this.length));
        }
    }
}