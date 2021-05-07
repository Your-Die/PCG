namespace Chinchillada.Generation.Turtle
{
    using System.Collections.Generic;
    using Chinchillada;
    using Generation;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    public class LSystemJob : GeneratorComponentBase<IEnumerable<char>>
    {
        [SerializeField] private string input;

        [SerializeField] private int iterations;

        [OdinSerialize, Required] private ISource<LSystem> system;

        public int Iterations
        {
            get => this.iterations;
            set => this.iterations = value;
        }

        protected override IEnumerable<char> GenerateInternal()
        {
            return this.system.Get().Rewrite(this.input, this.iterations);
        }
    }
}