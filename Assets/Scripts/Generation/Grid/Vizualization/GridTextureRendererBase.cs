using Chinchillada.Colors;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public abstract class GridTextureRendererBase : GridRendererBase
    {
        [SerializeField] private IColorScheme colorScheme;
        [SerializeField] protected FilterMode filterMode;

        protected override void RenderGrid(IntGrid2D newGrid)
        {
            var texture = this.ConvertToTexture(newGrid);
            this.SetTexture(texture);
        }

        private Texture2D ConvertToTexture(IntGrid2D grid)
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

        protected abstract void SetTexture(Texture texture);
    }
}