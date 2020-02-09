using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Evolution.Chromosome
{
    public abstract class ChromosomeInterpreter<T> : ChinchilladaBehaviour, IChromosomeInterpreter
    {
        [SerializeField, FindComponent] private T target;

        public abstract int ChromosomeLength { get; }
        
        public void Interpret(Chromosome chromosome) => this.Interpret(this.target, chromosome);


        protected abstract void Interpret(T target, Chromosome chromosome);
    }
}