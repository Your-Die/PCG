﻿using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation
{
    public class TextureRenderer : ChinchilladaBehaviour, IVisualizer<Texture2D>
    {
        [SerializeField] private FilterMode filterMode;

        [SerializeField] private bool destroyOldTextures;
        
        [SerializeField, FindComponent] private new Renderer renderer;
        
        public void Visualize(Texture2D texture)
        {
            var material = this.renderer.material;

            if (this.destroyOldTextures && material.mainTexture != texture) 
                Destroy(material.mainTexture);

            material.mainTexture = texture;
            material.mainTexture.filterMode = this.filterMode;

            this.Content = texture;
        }

        public Texture2D Content { get; private set; }
    }
}