using Chinchillada.Foundation;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class TextureRenderer : ChinchilladaBehaviour, IRenderer<Texture2D>
    {
        [SerializeField] private FilterMode filterMode;

        [SerializeField, FindComponent] private new Renderer renderer;
        
        public void Render(Texture2D texture)
        {
            var material = this.renderer.material;

            material.mainTexture = texture;
            material.mainTexture.filterMode = this.filterMode;
        }
    }
}