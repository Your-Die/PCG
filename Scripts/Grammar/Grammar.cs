using System.Linq;

namespace Chinchillada.Generation.Grammar
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Chinchillada/Grammar/Grammar")]
    public class Grammar : ScriptableObject
    {
        [SerializeField] private List<Rule> rules;

        public IEnumerable<Rule> Rules => this.rules;

        private void OnValidate()
        {
            var symbolLookup = this.rules.ToLookup(rule => rule.LHS);

            foreach (var grouping in symbolLookup)
            {
                var ruleGroup = grouping.ToList();

                if (ruleGroup.Count > 1) 
                    Combine(ruleGroup);
            }

            void Combine(IList<Rule> ruleGroup)
            {
                var firstRule = ruleGroup[0];
                
                for (var i = ruleGroup.Count - 1; i >= 1; i--)
                {
                    var rule = ruleGroup[i];
                    var rhs = rule.RHS;

                    foreach (var expansion in rhs) 
                        firstRule.RHS.Add(expansion);

                    this.rules.Remove(rule);
                }
            }
        }
    }
}