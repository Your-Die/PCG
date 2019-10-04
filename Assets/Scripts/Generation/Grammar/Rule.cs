namespace Chinchillada.Generation.Grammar
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Grammar/Rule")]
    public class Rule : ScriptableObject
    {
        [SerializeField] private Symbol leftHandSide;

        [SerializeField] private List<Expansion> rightHandSide;
        
        public Symbol LHS => leftHandSide;

        public IEnumerable<Expansion> RHS => rightHandSide;
    }
}