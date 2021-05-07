namespace Chinchillada.Generation.Turtle
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    [Serializable]
    public class CharacterStream : IEnumerable<char>
    {
        public IEnumerable<char> Characters { get; }

        public CharacterStream(IEnumerable<char> characters) => this.Characters = characters;
        
        public IEnumerator<char> GetEnumerator() => this.Characters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.Characters).GetEnumerator();
    }
}