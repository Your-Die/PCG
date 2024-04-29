using System;
using Chinchillada;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.PCG
{
    public class GeneratorComponent<T> : AutoRefBehaviour, IGenerator<T>
    {
        [SerializeField] private IGenerator<T> generator;

        [Button]
        public T Generate(IRNG random) => this.generator.Generate(random);
    }
}