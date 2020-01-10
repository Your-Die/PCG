using Chinchillada.Colors;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class GridTextureRenderer : GridRendererBase
    {
        [SerializeField, FindComponent] private Renderer textureRenderer;
        
        [SerializeField] private IColorScheme colorScheme;

        [SerializeField] private FilterMode filterMode;
        
        protected override void RenderGrid(Grid2D newGrid)
        {
            var texture = this.ConvertToTexture(newGrid);
            this.SetTexture(texture);
        }

        private Texture2D ConvertToTexture(Grid2D grid)
        {
            var texture = new Texture2D(grid.Width, grid.Height);
            
            for (var x = 0; x < grid.Width; x++)
            for (var y = 0; y < grid.Height; y++)
            {
                var item = grid[x, y];
                var color = this.colorScheme[item];
                
                texture.SetPixel(x, y, color);
            }
            
            texture.Apply();
            return texture;
        }

        private void SetTexture(Texture texture)
        {
            var material = this.textureRenderer.material;
            
            material.mainTexture = texture;
            material.mainTexture.filterMode = this.filterMode;
        }

    }
}