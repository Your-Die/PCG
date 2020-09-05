using System.Linq;
using Chinchillada.Colors;
using UnityEngine;

namespace Chinchillada.GeneratorGraph.ColorSchemes
{
    public class AnalogousSchemeNode : ColorschemeGeneratorNode
    {
        [Input, SerializeField] private int hueCount = 3;

        [Input, SerializeField] private float span = 0.125f;
        
        [SerializeField] private MonochromeSchemeGenerator monochromeGenerator;
        
        protected override void UpdateInputs()
        {
            this.hueCount = this.GetInputValue(nameof(this.hueCount), this.hueCount);
            this.span = this.GetInputValue(nameof(this.span), this.span);
        }

        protected override ColorScheme GenerateColorScheme()
        {
            var halfSpan = this.span / 2;

            var spanMid = HSVColor.RandomHue();

            var spanMin = spanMid - halfSpan;
            var spanMax = spanMid + halfSpan;

            var colorSchemes = new ColorScheme[this.hueCount];

            for (var index = 0; index < this.hueCount; index++)
            {
                var point = (float) index / this.hueCount;

                var hue = Mathf.Lerp(spanMin, spanMax, point);

                if (hue < 0)
                {
                    hue = 1 - Mathf.Abs(hue) % 1;
                }
                else if (hue > 1)
                {
                    hue %= 1;
                }

                colorSchemes[index] = this.monochromeGenerator.Generate(hue);
            }

            return colorSchemes.Aggregate((scheme, other) => scheme + other);
        }
    }
}