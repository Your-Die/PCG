using Chinchillada.Generation;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Chinchillada.Colors
{
    public class ColorSchemeGizmo : ChinchilladaBehaviour
    {
        [SerializeField] private IGenerator<ColorScheme> schemeGenerator;

        [SerializeField] private float cubeSize = 1;

        private ColorScheme scheme;
        
        [Button]
        public void Generate() => this.scheme = this.schemeGenerator.Generate();

        [Button]
        public void Save()
        {
            var scriptableScheme = ScriptableObjectHelper.CreateAsset<ScriptableScheme>();
            scriptableScheme.CopyFrom(this.scheme);
        }
        
        private void OnDrawGizmos()
        {
            if (this.scheme == null)
                return;

            var bottomLeft = this.transform.position;
            
            for (var index = 0; index < this.scheme.Count; index++)
            {
                var position = bottomLeft + this.cubeSize * index * Vector3.right;
                var size = Vector3.one * this.cubeSize;

                Gizmos.color = this.scheme[index];
                Gizmos.DrawCube(position, size);
            }
        }
    }
}