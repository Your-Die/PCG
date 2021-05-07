namespace Chinchillada.Generation.Turtle
{
    using System;
    using System.Linq;
    using Chinchillada;
    using Chinchillada.Generation.Evolution;
    using Turtle;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class SparsityEvaluator : IMetricEvaluator<Texture2D>
    {
        [OdinSerialize] private ISource<Color> emptyColorSource;

        [OdinSerialize] private IColorComparer colorComparer = new ColorComparer();

        public float Evaluate(Texture2D texture)
        {
            var emptyColor = this.emptyColorSource.Get();
            var pixels     = texture.GetPixels();

            var emptyCount = pixels.Count(IsEmpty);
            return (float) emptyCount / pixels.Length;

            bool IsEmpty(Color pixel) => this.colorComparer.AreAlmostEqual(pixel, emptyColor);
        }
    }
}