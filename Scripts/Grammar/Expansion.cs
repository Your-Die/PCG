namespace Chinchillada.Generation.Grammar
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Expansion
    {
        [SerializeField] private List<Symbol> symbols;

        public IEnumerable<Symbol> Symbols => symbols;
    }
}