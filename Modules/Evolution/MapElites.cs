using System;
using System.Collections.Generic;
using Chinchillada;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Chinchillada.PCG.Evolution
{
    public class MapElites<T> : AutoRefBehaviour, IParentSelector, ISurvivorSelector
    {
        [SerializeField] private MetricAxis diversityX;
        [SerializeField] private MetricAxis diversityY;

        [SerializeField, Required, FindComponent]
        private Evolution<T> evolution;

        [SerializeReference] private IRNG random = new UnityRandom();
        
        public Genotype<T>[,] Map { get; private set; }

        public int Width => this.diversityX.BinCount;
        public int Height => this.diversityY.BinCount;
        
        public event Action PopulationChanged;

        public IEnumerable<IGenotype> SelectParents(IEnumerable<IGenotype> _)
        {
            while (true)
            {
                var x = this.random.Range(this.Width);
                var y = this.random.Range(this.Height);

                yield return this.Map[x, y];
            }
        }

        public IEnumerable<IGenotype> SelectSurvivors(IEnumerable<IGenotype> _, IEnumerable<IGenotype> children)
        {
            this.Evaluate(children);
            return this.EnumerateElites();
        }

        private void Evaluate(IEnumerable<IGenotype> population)
        {
            var typedPopulation = population.Convert(candidate => (Genotype<T>) candidate);
            this.Evaluate(typedPopulation);
        }

        private void Evaluate(IEnumerable<Genotype<T>> population)
        {
            foreach (var genotype in population)
            {
                var candidate = genotype.Candidate;
                
                var x = this.diversityX.Evaluate(candidate);
                var y = this.diversityY.Evaluate(candidate);

                if (this.Map[x, y] == null || this.Map[x, y].Fitness < genotype.Fitness)
                    this.Map[x, y] = genotype;
            }

            this.PopulationChanged?.Invoke();
        }

        private IEnumerable<Genotype<T>> EnumerateElites()
        {
            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
                if (this.Map[x, y] != null)
                    yield return this.Map[x, y];
        }

        private void InitializeMap() => this.Map = new Genotype<T>[this.Width, this.Height];

        private void OnInitialPopulationGenerated(IEnumerable<Genotype<T>> population)
        {
            this.InitializeMap();
            this.Evaluate(population);
        }

        private void OnEnable() => this.evolution.InitialPopulationGenerated += this.OnInitialPopulationGenerated;
        private void OnDisable() => this.evolution.InitialPopulationGenerated -= this.OnInitialPopulationGenerated;

  
        [Serializable]
        private class MetricAxis
        {
            [SerializeField] private IMetricEvaluator<T> metric;

            [SerializeField] private Vector2 range = new Vector2(0, 1);

            [SerializeField] private int binCount = 10;

            private float BinSize => (this.range.y - this.range.x) / this.binCount;

            public int BinCount => this.binCount;

            public int Evaluate(T candidate)
            {
                var result = this.metric.Evaluate(candidate);
                result = this.range.RangeClamp(result);

                return (int) (result / this.BinSize);
            }
        }
    }
}