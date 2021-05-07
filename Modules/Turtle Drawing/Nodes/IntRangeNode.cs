namespace Ints
{
    using Generators;
    using UnityEngine;

    public class IntRangeNode : GeneratorNode<int>
    {
        [SerializeField] [Input] private int minimum;
        [SerializeField] [Input] private int maximum;
        
        protected override int GenerateInternal()
        {
            return this.Random.Range(this.minimum, this.maximum);
        }

        protected override void UpdateInputs()
        {
            base.UpdateInputs();
            
            this.UpdateInput(ref this.minimum, nameof(this.minimum));
            this.UpdateInput(ref this.maximum, nameof(this.maximum));
        }
    }
}