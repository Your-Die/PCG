using System;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class GeneratorComponent<T> : ChinchilladaBehaviour, IGenerator<T>
    {
        [SerializeField] private IGenerator<T> generator;
        public T Result => this.generator.Result;

        public event Action<T> Generated
        {
            add => this.generator.Generated += value;
            remove => this.generator.Generated -= value;
        }

        [Button]
        public T Generate() => this.generator.Generate();

        T ISource<T>.Get() => this.generator.Get();
    }
}