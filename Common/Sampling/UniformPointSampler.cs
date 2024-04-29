namespace Chinchillada.PCG.Sampling
{
    using System.Collections.Generic;
    using UnityEngine;

    public class UniformPointSampler : GeneratorBase<IEnumerable<Vector2>>
    {
        protected override IEnumerable<Vector2> GenerateInternal(IRNG random)
        {
            while (true)
            {
                yield return new Vector2
                {
                    x = random.Float(),
                    y = random.Float()
                };
            }
        }
    }
}