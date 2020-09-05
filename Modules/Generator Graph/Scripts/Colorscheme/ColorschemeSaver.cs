using Chinchillada.Colors;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace Chinchillada.GeneratorGraph.ColorSchemes

{
    public class ColorschemeSaver : Node
    {
        [SerializeField, Input] private ColorScheme colorScheme;

        [SerializeField, InlineEditor] private ScriptableScheme output;

        [Button]
        public void Save()
        {
            this.colorScheme = this.GetInputValue(nameof(this.colorScheme), this.colorScheme);
            this.output.CopyFrom(this.colorScheme);
        }
    }
}