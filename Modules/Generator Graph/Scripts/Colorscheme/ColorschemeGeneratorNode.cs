using System.Collections.Generic;
using System.Linq;
using Chinchillada.Colors;
using Chinchillada.GeneratorGraph.Grid;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.GeneratorGraph.ColorSchemes

{
    public abstract class ColorschemeGeneratorNode : GeneratorNode<ColorScheme>
    {
        [SerializeField, ReadOnly, UsedImplicitly, HideLabel, PropertyOrder(int.MaxValue)]
        [ListDrawerSettings(Expanded = true, HideAddButton = true, HideRemoveButton = true)]
        private List<Color> preview;

        protected override ColorScheme GenerateInternal() => this.GenerateColorScheme();

        protected abstract ColorScheme GenerateColorScheme();

        protected override void RenderPreview(ColorScheme result) => this.preview = result.ToList();
    }
}