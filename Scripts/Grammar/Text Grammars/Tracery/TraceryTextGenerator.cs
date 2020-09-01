using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using Chinchillada.Generation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityTracery;

namespace Mutiny.Grammar
{
    public class TraceryTextGenerator : GeneratorComponentBase<string>
    {
        [SerializeField, InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        private IGrammarDefinition grammarDefinition;

        [SerializeField] private List<GrammarActionSet> actionsToInject;

        [SerializeField] private bool updateGrammarBeforeGeneration;

        [SerializeField] private bool logResult;

        private TraceryGrammar grammar;

        public IGrammarDefinition GrammarDefinition
        {
            get => this.grammarDefinition;
            set => this.grammarDefinition = value;
        }

        public IEnumerable<TraceryAction> Actions => this.actionsToInject.SelectMany(set => set.Actions);

        public string Generate(TraceryAction action) => this.Generate(Enumerables.Single(action));

        public string Generate(IEnumerable<TraceryAction> actions)
        {
            // Temp. disable update as we do it manually.
            var updateGrammar = this.updateGrammarBeforeGeneration;
            this.updateGrammarBeforeGeneration = false;
            
            // Update grammar with given actions.
            this.UpdateGrammar(actions.Concat(this.Actions));
            
            // Generate.
            var result = this.Generate();

            // Restore update setting.
            this.updateGrammarBeforeGeneration = updateGrammar;

            return result;
        }

        protected override string GenerateInternal()
        {
            if (this.updateGrammarBeforeGeneration)
                this.UpdateGrammar();

            var result = this.grammar.Generate();

            if (this.logResult)
                Debug.Log(result);

            return result;
        }

        [Button]
        private void UpdateGrammar()
        {
            this.UpdateGrammar(this.Actions);
        }

        private void UpdateGrammar(IEnumerable<TraceryAction> actions)
        {
            this.grammar = TraceryFactory.Construct(this.grammarDefinition, actions);
        }
    }
}