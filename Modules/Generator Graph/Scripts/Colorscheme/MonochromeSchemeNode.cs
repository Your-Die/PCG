using Chinchillada.Colors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.GeneratorGraph.ColorSchemes

{
    public class MonochromeSchemeNode : ColorschemeGeneratorNode
    {
        [SerializeField] private int colorCount = 3;

        [SerializeField, MinMaxSlider(0, 1)] 
        private Vector2 valueRange = new Vector2(0, 1);

        [SerializeField, MinMaxSlider(0, 1)] 
        private Vector2 saturationRange = new Vector2(1, 1);
        
        protected override void UpdateInputs()
        {
            this.colorCount = this.GetInputValue(nameof(this.colorCount), this.colorCount);
            this.valueRange = this.GetInputValue(nameof(this.valueRange), this.valueRange);
            this.saturationRange = this.GetInputValue(nameof(this.saturationRange), this.saturationRange);
        }

        protected override ColorScheme GenerateColorScheme()
        {
            return MonochromeSchemeGenerator.GenerateMonochromeScheme(this.colorCount, this.valueRange, this.saturationRange);
        }
    }
}