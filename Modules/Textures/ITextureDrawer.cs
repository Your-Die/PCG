using UnityEngine;

namespace Chinchillada.Generation.Textures
{
    public interface ITextureDrawer
    {
        Texture2D Texture { get; set; }
        void DrawPixel(Vector2Int coordinate, Color      color);
        void DrawLine(Vector2Int  from,       Vector2Int to, Color color);
    }
}