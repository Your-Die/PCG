namespace Chinchillada.Generation.Textures
{
    using System;
    using Chinchillada;
    using UnityEngine;

    [Serializable]
    public class TextureDrawer : ITextureDrawer
    {
        public Texture2D Texture { get; set; }

        public void DrawPixel(Vector2Int coordinate, Color color)
        {
            this.Texture.SetPixel(coordinate.x, coordinate.y, color);
        }

        public void DrawLine(Vector2Int @from, Vector2Int to, Color color)
        {
            this.Texture.DrawLine(from, to, color);
        }
    }
}