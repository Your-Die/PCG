using System;
using Chinchillada.Generation;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Colors
{
    [Serializable]
    public class MonochromeSchemeGenerator : IGenerator<ColorScheme>
    {
        [SerializeField] private int colorCount = 3;

        [SerializeField, MinMaxSlider(0, 1)] private Vector2 valueRange = new Vector2(0, 1);

        [SerializeField, MinMaxSlider(0, 1)] private Vector2 saturationRange = new Vector2(1, 1);


        public ColorScheme Result { get; private set; }
        public event Action<ColorScheme> Generated;

        public ColorScheme Generate()
        {
            var hue = HSVColor.RandomHue();
            this.Result = this.Generate(hue);

            this.Generated?.Invoke(this.Result);
            return this.Result;
        }

        public ColorScheme Generate(float hue)
        {
            return GenerateMonochromeScheme(hue, this.colorCount, this.valueRange, this.saturationRange);
        }


        public static ColorScheme GenerateMonochromeScheme(float hue, int colorCount, Vector2 valueRange,
            Vector2 saturationRange)
        {
            var colors = new HSVColor[colorCount];

            for (var index = 0; index < colorCount; index++)
            {
                var saturation = saturationRange.RandomInRange();
                var value = valueRange.RangeLerp((float) index / colorCount);

                colors[index] = new HSVColor
                {
                    Hue = hue,
                    Saturation = saturation,
                    Value = value
                };
            }

            return new ColorScheme(colors);
        }

        public static ColorScheme GenerateMonochromeScheme(int colorCount, Vector2 valueRange, Vector2 saturationRange)
        {
            var hue = HSVColor.RandomHue();
            return GenerateMonochromeScheme(hue, colorCount, valueRange, saturationRange);
        }
    }
}