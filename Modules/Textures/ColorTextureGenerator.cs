namespace Chinchillada.PCG.Textures
{
    using System;
    using PCG;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class ColorTextureGenerator : GeneratorBase<Texture2D>
    {
        [SerializeField] private Vector2Int resolution;

        [OdinSerialize] private ISource<Color> colorSource;
        
        protected override Texture2D GenerateInternal(IRNG random)
        {
            var texture = new Texture2D(this.resolution.x, this.resolution.y);

            var color = this.colorSource.Get();

            var pixels = texture.GetPixels();

            for (var i = 0; i < pixels.Length; i++)
                pixels[i] = color;

            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }
    }
}