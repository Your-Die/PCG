namespace Chinchillada.PCG.Sampling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class BestCandidatePointSampler : GeneratorBase<IEnumerable<Vector2>>
    {
        [SerializeField] private int candidateCount = 10;

        [OdinSerialize] private IGenerator<IEnumerable<Vector2>> subSampler = new UniformPointSampler();
        
        protected override IEnumerable<Vector2> GenerateInternal(IRNG random)
        {
            var memory = new List<Vector2>();

            while (true)
            {
                var points = this.subSampler.Generate(random).Take(this.candidateCount);
                var point  = points.ArgMax(DistanceToExisting);

                yield return point;
                memory.Add(point);
            }

            float DistanceToExisting(Vector2 point)
            {
                return memory.Any() 
                    ? memory.Min(other => Vector2.Distance(point, other)) 
                    : 0;
            }
        }
    }
}