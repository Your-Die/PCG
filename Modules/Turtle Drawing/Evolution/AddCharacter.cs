namespace Chinchillada.Generation.Turtle
{
    using System.Collections.Generic;
    using System.Text;
    using Chinchillada;
    using Chinchillada.Generation.Evolution;
    using Sirenix.Serialization;

    public class AddCharacter : IMutator<string>
    {
        [OdinSerialize] private IGenerator<char> characterGenerator;


        public string Mutate(string parent, IRNG random)
        {
            var character = this.characterGenerator.Generate();

            var builder = new StringBuilder(parent.Length);
            builder.Append(character);
            
            return builder.ToString();
        }
    }
}