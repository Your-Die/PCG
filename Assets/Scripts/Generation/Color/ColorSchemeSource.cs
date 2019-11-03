using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Colors
{
    public class ColorSchemeSource : SerializedMonoBehaviour, IColorScheme
    {
        [SerializeField] private IColorScheme scheme;
        
        public int Count => this.scheme.Count;
        
        public Color this[int index] => this.scheme[index];
        
        public IEnumerator<Color> GetEnumerator() => this.scheme.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}