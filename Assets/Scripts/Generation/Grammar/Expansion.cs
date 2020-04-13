namespace Chinchillada.Generation.Grammar
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Expansion
    {
        [SerializeField] private List<Symbol> symbols;

        public IEnumerable<Symbol> Symbols => this.symbols;

        public override string ToString()
        {
            return string.Join(string.Empty, this.symbols);
        }
    }
}