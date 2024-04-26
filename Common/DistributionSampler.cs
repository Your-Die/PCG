﻿using System;
using Chinchillada.Distributions;
using UnityEngine;

namespace Chinchillada.PCG
{
    [Serializable]
    public class DistributionSampler<T> : GeneratorBase<T>
    {
        [SerializeField] private IDistribution<T> distribution;

        protected override T GenerateInternal() => this.distribution.Sample(this.Random);
    }
}