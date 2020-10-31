using System;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable]
    public abstract class Mutator<T> : IMutator<T>
    {
        [SerializeField, Range(0, 1)] private float chance;

        public float Chance => this.chance;
        
        public abstract T Mutate(T parent);
    }
}