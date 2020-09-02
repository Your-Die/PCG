﻿using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mutiny.Grammar
{
    public class GrammarDefinitionMerger : ChinchilladaBehaviour
    {
        [SerializeField] private List<GrammarDefinition> inputs;

        [SerializeField, InlineEditor] private GrammarDefinition output;

        [Button]
        public GrammarDefinition Merge()
        {
            var rules = this.inputs.SelectMany(definition => definition.Rules);
            this.output.Rules = rules.ToList();

            return this.output;
        }
    }
}