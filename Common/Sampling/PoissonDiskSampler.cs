namespace Chinchillada.PCG.Sampling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [Serializable]
    public class PoissonDiskSampler : GeneratorBase<IEnumerable<Vector2>>
    {
        [SerializeField, Range(0, 1)] private float minimumDistance = 0.01f;

        [SerializeField] private int sampleAttempts = 10;
        
        protected override IEnumerable<Vector2> GenerateInternal()
        {
            var firstPoint = new Vector2
            {
                x = this.Random.Float(),
                y = this.Random.Float()
            };

            var memory     = new List<Vector2> {firstPoint};
            var active     = new List<Vector2> {firstPoint};

            var annulus = new Vector2(this.minimumDistance, this.minimumDistance * 2);
            
            while (active.Any())
            {
                var pointIndex = active.ChooseRandomIndex();
                var point      = active[pointIndex];

                if (TrySampleNear(point, out var newPoint))
                {
                    yield return newPoint;
                    
                    memory.Add(newPoint);
                    active.Add(newPoint);
                }
                else
                {
                    active.RemoveAt(pointIndex);
                }
            }

            bool TrySampleNear(Vector2 point, out Vector2 result)
            {
                for (var i = 0; i < this.sampleAttempts; i++)
                {
                    var distance  = annulus.RandomInRange(this.Random);
                    var direction = this.Random.Direction();

                    result = point + direction * distance;
                    if (IsValid(result))
                        return true;
                }

                result = default;
                return false;
            }

            bool IsValid(Vector2 point)
            {
                return memory.All(other => Vector2.Distance(point, other) >= this.minimumDistance);
            }
        }
    }
}