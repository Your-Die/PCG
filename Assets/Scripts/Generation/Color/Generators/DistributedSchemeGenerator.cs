using System.Linq;
using Chinchillada.Generation;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Colors
{
    /// <summary>
    /// Generates a color scheme where the hues are evenly distributed along the hue wheel.
    /// </summary>
    public class DistributedSchemeGenerator : ChinchilladaBehaviour, IGenerator<ColorScheme>
    {
        [SerializeField] private int hueCount = 2;

        [SerializeField, FindComponent]
        private MonochromeSchemeGenerator monochromeGenerator;
        
        public ColorScheme Generate()
        {
            var segmentPerHue = 1f / this.hueCount;
            var baseHue = HSVColor.RandomHue();
            
            var schemes = new ColorScheme[this.hueCount];
            for (int index = 0; index < this.hueCount; index++)
            {
                var hue = baseHue + index * segmentPerHue;
                hue %= 1;

                schemes[index] = this.monochromeGenerator.Generate(hue);
            }

            return schemes.Aggregate((scheme1, scheme2) => scheme1 + scheme2);
        }
    }
}