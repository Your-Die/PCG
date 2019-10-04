namespace Chinchillada.Generation.Grammar
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Grammar/Grammar")]
    public class Grammar : ScriptableObject
    {
        [SerializeField] private List<Rule> rules;

        public IEnumerable<Rule> Rules => rules;
    }
}