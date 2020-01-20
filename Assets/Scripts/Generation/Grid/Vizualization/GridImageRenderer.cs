using UnityEngine;
using UnityEngine.UI;

namespace Chinchillada.Generation.Grid
{
    public class GridImageRenderer : GridTextureRendererBase
    {
        [SerializeField] private RawImage image;
        
        protected override void SetTexture(Texture texture)
        {
            this.image.texture = texture;
        }
    }
}