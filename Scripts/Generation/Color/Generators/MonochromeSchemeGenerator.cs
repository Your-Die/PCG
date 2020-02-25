using Chinchillada.Generation;
using Chinchillada.Utilities;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chinchillada.Colors
{
    public class MonochromeSchemeGenerator : GeneratorBase<ColorScheme>
    {
        [SerializeField] private int colorCount = 3;

        [SerializeField, MinMaxSlider(0, 1)] 
        private Vector2 valueRange = new Vector2(0, 1);

        [SerializeField, MinMaxSlider(0, 1)] 
        private Vector2 saturationRange = new Vector2(1, 1);

        public ColorScheme Generate(float hue) => this.Generate(hue, this.colorCount);

        public ColorScheme Generate(float hue, int amount)
        {
            var colors = new HSVColor[amount];

            for (var index = 0; index < amount; index++)
            {
                var saturation = this.saturationRange.RandomInRange();
                var value = this.valueRange.RangeLerp((float) index / amount);

                colors[index] = new HSVColor
                {
                    Hue = hue,
                    Saturation = saturation,
                    Value = value
                };
            }

            return new ColorScheme(colors);
        }
        
        protected override ColorScheme GenerateInternal()
        {
            var hue = HSVColor.RandomHue();
            return this.Generate(hue, this.colorCount);
        }
    }
}