using System;

namespace Chinchillada.Generation.Grammar
{
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Rule
    {
        [SerializeField] private Symbol leftHandSide;

        [SerializeField] private List<Expansion> rightHandSide;
        
        public Symbol LHS => leftHandSide;

        public IList<Expansion> RHS => this.rightHandSide;
    }
}