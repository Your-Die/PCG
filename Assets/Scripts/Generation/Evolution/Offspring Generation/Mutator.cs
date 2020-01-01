using System;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    [Serializable]
    public abstract class Mutator<T> : ChinchilladaBehaviour
    {
        [SerializeField, Range(0, 1)] private float chance;

        public float Chance => this.chance;
        
        public abstract T Mutate(T parent);
    }
}