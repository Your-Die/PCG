namespace Chinchillada.Turtle
{
    using System.Collections.Generic;
    using Chinchillada;
    using Generation;
    using Sirenix.Serialization;
    using UnityEngine;

    public class TurtleTextureGenerator : AsyncGeneratorBase<Texture2D>
    {
        [OdinSerialize] private TurtleRunner runner;

        [OdinSerialize] private ISource<IEnumerable<char>> textSource;

        [OdinSerialize] private ISource<Texture2D> textureSource;

        [OdinSerialize] private ISource<Color> drawColorSource;

        public override IEnumerable<Texture2D> GenerateAsync()
        {
            var texture = this.textureSource.Get();
            var text    = this.textSource.Get();
            var color   = this.drawColorSource.Get();

            var actions = TurtleScanner.Scan(text);
            
            return this.runner.RunIteratively(actions, texture, color);
        }
    }
}