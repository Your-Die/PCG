using System;
using System.Collections.Generic;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    public class MapElites<T> : ChinchilladaBehaviour, IParentSelector, ISurvivorSelector
    {
        [SerializeField] private MetricAxis diversityX;
        [SerializeField] private MetricAxis diversityY;

        [SerializeField, Required, FindComponent]
        private Evolution<T> evolution;

        private readonly List<Genotype<T>> elites = new List<Genotype<T>>();

        public int?[,] map { get; private set; }

        public Genotype<T> this[int x, int y]
        {
            get
            {
                var index = this.map[x, y];
                return index != null
                    ? this.elites[index.Value]
                    : null;
            }
        }

        public int Width => this.diversityX.BinCount;
        public int Height => this.diversityY.BinCount;

        public event Action PopulationChanged;

        public IEnumerable<IGenotype> SelectParents(IEnumerable<IGenotype> _)
        {
            while (true)
                yield return this.elites.ChooseRandom();
        }

        public IEnumerable<IGenotype> SelectSurvivors(IEnumerable<IGenotype> _, IEnumerable<IGenotype> children)
        {
            this.Evaluate(children);
            return this.elites;
        }

        private void Evaluate(IEnumerable<IGenotype> population)
        {
            var typedPopulation = population.Convert(candidate => (Genotype<T>) candidate);
            this.Evaluate(typedPopulation);
        }

        private void Evaluate(IEnumerable<Genotype<T>> population)
        {
            var elitesChanged = false;

            foreach (var genotype in population)
            {
                var candidate = genotype.Candidate;

                var x = this.diversityX.Evaluate(candidate);
                var y = this.diversityY.Evaluate(candidate);

                if (this.map[x, y] == null)
                {
                    var index = this.elites.Count;
                    this.elites.Add(genotype);

                    this.map[x, y] = index;
                    elitesChanged = true;
                }
                else
                {
                    var index = this.map[x, y].Value;
                    var competitor = this.elites[index];

                    if (genotype.Fitness <= competitor.Fitness) 
                        continue;
                    
                    this.elites[index] = genotype;
                    elitesChanged = true;
                }
            }

            if (elitesChanged)
                this.PopulationChanged?.Invoke();
        }

        private void InitializeMap() => this.map = new int?[this.Width, this.Height];

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
                var bin = (int) (result / this.BinSize);
                
                return Mathf.Clamp(bin, 0, this.binCount - 1);
            }
        }
    }
}