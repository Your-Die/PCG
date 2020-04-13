namespace Chinchillada.Generation.Grammar
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Grammar/Symbol")]
    public class Symbol : ScriptableObject
    {
        public override string ToString() => this.name;
    }
}