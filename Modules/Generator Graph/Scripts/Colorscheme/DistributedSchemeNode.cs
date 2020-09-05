using System.Linq;
using Chinchillada.Colors;
using UnityEngine;

namespace Chinchillada.GeneratorGraph.ColorSchemes

{
    public class DistributedSchemeNode : ColorschemeGeneratorNode
    {
        [Input, SerializeField] private int hueCount = 2;

        [SerializeField] private MonochromeSchemeGenerator monochromeGenerator;

        protected override void UpdateInputs()
        {
            this.hueCount = this.GetInputValue(nameof(this.hueCount), this.hueCount);

        }

        protected override ColorScheme GenerateColorScheme()
        {
            var segmentPerHue = 1f / this.hueCount;
            var baseHue = HSVColor.RandomHue();

            var schemes = new ColorScheme[this.hueCount];
            for (var index = 0; index < this.hueCount; index++)
            {
                var hue = baseHue + index * segmentPerHue;
                hue %= 1;

                schemes[index] = this.monochromeGenerator.Generate(hue);
            }

            return schemes.Aggregate((scheme1, scheme2) => scheme1 + scheme2);
        }
    }
}