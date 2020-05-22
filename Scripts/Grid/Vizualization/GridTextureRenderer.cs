using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class GridTextureRenderer : GridTextureRendererBase
    {
        [SerializeField, FindComponent] protected Renderer textureRenderer;

        protected override void SetTexture(Texture texture)
        {
            var material = this.textureRenderer.material;

            material.mainTexture = texture;
            material.mainTexture.filterMode = this.filterMode;
        }
    }
}