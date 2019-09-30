using System;
using Chinchillada.Distributions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    public class Perceptron
    {
        private readonly float learningRate;
        private readonly Func<float, float> activation;
        private readonly float[] weights;

        private int InputCount { get; }

        public Perceptron(int inputCount, float learningRate, float weightMin, float weightMax,
            Func<float, float> activation)
        {
            this.InputCount = inputCount;
            this.learningRate = learningRate;
            this.activation = activation;

            this.weights = new float[inputCount];

            for (var i = 0; i < inputCount; i++)
                this.weights[i] = Random.Range(weightMin, weightMax);
        }

        public void Train(TrainingData data)
        {
            bool changed;

            do
            {
                var (input, output) = data.Sample();
                changed = this.Train(input, output);
            } while (changed);
        }

        public float Classify(float[] input)
        {
            float output = 0;
            
            for (var i = 0; i < this.InputCount; i++)
            {
                var inputValue = input[i];
                var weight = this.weights[i];

                var signal = inputValue * weight;
                output += this.activation(signal);
            }

            return output;
        }

        private bool Train(float[] input, float expectedOutput)
        {
            var output = this.Classify(input);

            var error = expectedOutput - output;

            var changed = false;
            for (int i = 0; i < this.InputCount; i++)
            {
                var oldWeight = this.weights[i];
                var newWeight = oldWeight + this.learningRate * error * input[i];
                
                if (Mathf.Approximately(newWeight, oldWeight))
                    continue;

                this.weights[i] = newWeight;
                changed = true;
            }

            return changed;
        }
    }

    public class TrainingData
    {
        private readonly (float[] input, float output)[] data;
        private IDiscreteDistribution<int> distribution;

        public TrainingData((float[] input, float output)[] data)
        {
            this.data = data;
            this.distribution = data.IndexDistribution();
        }
        
        public (float[] input, float output) Sample()
        {
            var index = this.distribution.Sample();
            return this.data[index];
        }
    }
}