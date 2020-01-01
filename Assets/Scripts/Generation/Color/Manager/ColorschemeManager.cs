using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Behaviours;

namespace Chinchillada.Colors
{
    public class ColorschemeManager : SingleInstanceBehaviour<ColorschemeManager>, IColorScheme
    {
        [SerializeField] private IColorScheme colorScheme;

        public int Count => this.colorScheme.Count;

        public Color this[int index] => this.colorScheme[index];
        
        public IColorScheme Scheme
        {
            get => this.colorScheme;
            set => this.colorScheme = value;
        }

        public IEnumerator<Color> GetEnumerator() => this.colorScheme.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        protected override ColorschemeManager GetInstance() => this;
    }
}