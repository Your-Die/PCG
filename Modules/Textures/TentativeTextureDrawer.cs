namespace Chinchillada.Generation.Textures
{
    using System;
    using Chinchillada;
    using Sirenix.Serialization;
    using Turtle;
    using UnityEngine;

    [Serializable]
    public class TentativeTextureDrawer : ITextureDrawer
    {
        [OdinSerialize] private ISource<Color> freeColorSource;

        [OdinSerialize] private IColorComparer colorComparer = new ColorComparer();

        public Texture2D Texture { get; set; }

        public void DrawPixel(Vector2Int position, Color color)
        {
            var pixel = this.Texture.GetPixel(position.x, position.y);

            var freeColor = this.freeColorSource.Get();
            if (this.colorComparer.AreAlmostEqual(pixel, freeColor))
                this.Texture.SetPixel(position.x, position.y, color);
        }

        public void DrawLine(Vector2Int @from, Vector2Int to, Color color)
        {
            var dy = to.y - from.y;
            var dx = to.x - from.x;

            int stepX, stepY;

            if (dy < 0)
            {
                dy    = -dy;
                stepY = -1;
            }
            else
            {
                stepY = 1;
            }

            if (dx < 0)
            {
                dx    = -dx;
                stepX = -1;
            }
            else
            {
                stepX = 1;
            }

            dy <<= 1;
            dx <<= 1;

            float fraction;

            this.DrawPixel(from, color);
            if (dx > dy)
            {
                fraction = dy - (dx >> 1);
                while (Mathf.Abs(from.x - to.x) > 1)
                {
                    if (fraction >= 0)
                    {
                        from.y   += stepY;
                        fraction -= dx;
                    }

                    from.x   += stepX;
                    fraction += dy;
                    this.DrawPixel(from, color);
                }
            }
            else
            {
                fraction = dx - (dy >> 1);
                while (Mathf.Abs(from.y - to.y) > 1)
                {
                    if (fraction >= 0)
                    {
                        from.x   += stepX;
                        fraction -= dy;
                    }

                    from.y   += stepY;
                    fraction += dx;
                    this.DrawPixel(from, color);
                }
            }
        }
    }
}