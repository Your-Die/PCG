using System.Linq;
using Chinchillada.Generation;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Colors
{
    public class AnalogousSchemeGenerator : MonoBehaviour, IGenerator<ColorScheme>
    {
        [SerializeField] private int hueCount = 3;
        
        [SerializeField] private float span = 0.125f;

        [SerializeField, FindComponent]
        private MonochromeSchemeGenerator monochromeGenerator;
        
        public ColorScheme Generate()
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