using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.PCG
{
    [Serializable]
    public class SequenceGenerator<T> : GeneratorBase<IList<T>>
    {
        [SerializeField] private IGenerator<T> itemGenerator;
        
        [SerializeField] private int length;
        
        protected override IList<T> GenerateInternal(IRNG random)
        {
            var sequence = new T[this.length];
            
            for (var i = 0; i < this.length; i++) 
                sequence[i] = this.itemGenerator.Generate(random);

            return sequence;
        }
    }
}