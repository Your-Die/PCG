namespace Chinchillada.Generation.Turtle
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Chinchillada;
    using Chinchillada.Generation.Evolution;
    using Turtle;
    using Interfaces;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class TurtleEvolution : ChinchilladaBehaviour, IRoutine
    {
        [OdinSerialize] private Evolution<TurtleGenome> evolver;

        [SerializeField] private TurtleGenome.Decoder decoder;

        [SerializeField] private IVisualizer<Texture2D> visualizer;

        private RoutineHandler routineHandler;

        [Button]
        public void Execute()
        {
            this.routineHandler.StartRoutine(this.EvolutionRoutine());
        }

        public IEnumerator EvolutionRoutine()
        {
            var generations = this.evolver.EvolveGenerationWise();
            foreach (var fittestIndividual in generations)
            {
                var genome  = fittestIndividual.Candidate;
                var texture = this.decoder.Decode(genome);
                
                this.visualizer.Visualize(texture);
                yield return null;
            }
        }


        protected override void Awake()
        {
            base.Awake();
            this.routineHandler = new RoutineHandler(this);
        }

        IEnumerator IRoutine.Routine()
        {
            return this.EvolutionRoutine();
        }
    }

    [Serializable]
    public class TurtleGenome
    {
        [field: SerializeField] public string Pattern { get; }

        [field: SerializeField] public int Iterations { get; }

        public Texture2D Texture { get; set; }

        public TurtleGenome(string pattern, int iterations)
        {
            this.Pattern    = pattern;
            this.Iterations = iterations;
        }

        public IEnumerable<char> GenerateSequence()
        {
            var lSystem = new LSystem('F', this.Pattern);
            return lSystem.Rewrite("F", this.Iterations);
        }

        public class PatternMutator : IMutator<TurtleGenome>
        {
            [OdinSerialize] private IMutator<string> mutator;

            public TurtleGenome Mutate(TurtleGenome parent, IRNG random)
            {
                var pattern = this.mutator.Mutate(parent.Pattern, random);
                return new TurtleGenome(pattern, parent.Iterations);
            }
        }

        public class IterationMutator : IMutator<TurtleGenome>
        {
            [OdinSerialize] private IMutator<int> mutator;

            public TurtleGenome Mutate(TurtleGenome parent, IRNG random)
            {
                var iterations = this.mutator.Mutate(parent.Iterations, random);
                return new TurtleGenome(parent.Pattern, iterations);
            }
        }

        public class TurtleCrossover : ICrossover<TurtleGenome>
        {
            [OdinSerialize] private ICrossover<string> patternCrossover;

            public TurtleGenome Crossover(TurtleGenome parent1, TurtleGenome parent2, IRNG random)
            {
                var pattern = this.patternCrossover.Crossover(parent1.Pattern, parent2.Pattern, random);

                var iterations = random.Flip()
                    ? parent1.Iterations
                    : parent2.Iterations;

                return new TurtleGenome(pattern, iterations);
            }
        }


        [Serializable]
        public class PatternEvaluator : IMetricEvaluator<TurtleGenome>
        {
            [OdinSerialize, Required] private IMetricEvaluator<string> evaluator;

            public float Evaluate(TurtleGenome genome)
            {
                return this.evaluator.Evaluate(genome.Pattern);
            }
        }

        [Serializable]
        public class TextureEvaluator : IMetricEvaluator<TurtleGenome>
        {
            [OdinSerialize, Required] private IMetricEvaluator<Texture2D> textureEvaluator;

            [SerializeField, Required] private Decoder decoder = new Decoder();

            public float Evaluate(TurtleGenome genome)
            {
                genome.Texture = this.decoder.Decode(genome);
                return this.textureEvaluator.Evaluate(genome.Texture);
            }
        }

        public class Decoder
        {
            [OdinSerialize, Required] private TurtleTextureGenerator turtleTextureGenerator;

            [OdinSerialize, Required] private ISource<Texture2D> textureSource;

            [OdinSerialize] private ISource<Color> drawColorSource = new Constant<Color>(Color.black);

            [FoldoutGroup("References")] [OdinSerialize, Required, FindComponent(SearchStrategy.InScene)]
            private TurtleRunner runner;

            public Texture2D Decode(TurtleGenome genome)
            {
                if (genome.Texture)
                    return genome.Texture;

                var texture  = this.textureSource.Get();
                var color    = this.drawColorSource.Get();
                
                var sequence = genome.GenerateSequence();
                var actions  = TurtleScanner.Scan(sequence);
                
                this.runner.RunImmediate(actions, texture, color);

                return texture;
            }
        }
    }
}