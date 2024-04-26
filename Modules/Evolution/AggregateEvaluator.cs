namespace Chinchillada.PCG.Evolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.Serialization;
    using UnityEngine;

    public class AggregateEvaluator<T> : IMetricEvaluator<T>
    {
        [OdinSerialize] private List<WeightedEvaluator> evaluators;

        [OdinSerialize] private IAggregator<float> aggregator;

        public float Evaluate(T item)
        {
            var metrics = this.evaluators.Select(evaluator => evaluator.Evaluate(item));
            return this.aggregator.Aggregate(metrics);
        }

        [Serializable]
        private class WeightedEvaluator : IMetricEvaluator<T>
        {
            [OdinSerialize] private IMetricEvaluator<T> evaluator;

            [SerializeField] private float weight;
            public float Evaluate(T item)
            {
                var metric = this.evaluator.Evaluate(item);
                return metric * this.weight;
            }
        }
    }
}