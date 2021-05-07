namespace Chinchillada.Sampling
{
    using System;
    using Chinchillada;
    using UnityEngine;

    [Serializable]
    public class SmoothStart : ISampleModifier
    {
        [SerializeField] private int power = 2;

        public float Process(float sample, float _)
        {
            return MathHelper.Power(sample, this.power);
        }
    }
}