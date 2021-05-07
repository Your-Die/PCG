namespace Chinchillada.Generation.Sampling
{
    using System.Collections.Generic;
    using UnityEngine;

    public class UniformPointSampler : GeneratorBase<IEnumerable<Vector2>>
    {
        protected override IEnumerable<Vector2> GenerateInternal()
        {
            while (true)
            {
                yield return new Vector2
                {
                    x = this.Random.Float(),
                    y = this.Random.Float()
                };
            }
        }
    }
}