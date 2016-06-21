using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AI.Genetics;
using AI.NeuralNetworks;
using UnityEngine;

namespace AI.Genetics
{
    class Generation
    {
        enum FitnessMode
        {
            BiggerIsBetter,
            SmallerIsBetter
        }
        private Delegates.FitnessFunctor m_CalculateFitness;
        private string m_Id = System.Guid.NewGuid().ToString();
        private List<Genome> m_Population = new List<Genome>();
        private int m_PopulationSize;
        private List<int> m_NeuralNetConfiguration;
        private int m_KillCount;

        public Generation(int populationSize, int killCount, List<int> neuralNetConfig, Delegates.FitnessFunctor fitnessFunction)
        {
            m_NeuralNetConfiguration = neuralNetConfig;
            m_PopulationSize = populationSize;
            PopulateRandomly();
            m_PopulationSize = populationSize;
            m_KillCount = killCount;
            m_CalculateFitness = fitnessFunction;
        }

        public Generation(Generation other)
        {
            m_NeuralNetConfiguration = other.m_NeuralNetConfiguration;
            m_PopulationSize = other.m_PopulationSize;
            m_KillCount = other.m_KillCount;
            m_CalculateFitness = other.m_CalculateFitness;
            m_Population = other.m_Population.Clone();
        }

        public void PopulateRandomly()
        {
            int count = m_Population.Count;
            for (int i = 0; i < m_PopulationSize - count; i++)
            {
                m_Population.Add(new Genome(m_NeuralNetConfiguration));
            }
        }

        public void Evaluate()
        {
            foreach (Genome genome in m_Population)
            {
                genome.m_Fitness = m_CalculateFitness(genome);
            }
        }

        public void KillLast()
        {
            var count = m_Population.Count;
            m_Population.RemoveRange(count - m_KillCount, m_KillCount);
        }

        public void Mutate()
        {
            int count = m_Population.Count;
            for (int i = 0; i < count; i++)
            {
                Genome clone = (Genome)m_Population[i].Clone();
                Mutate(clone);
                m_Population.Add(clone);
            }
        }

        public void Mutate(Genome genome)
        {
            foreach(Layer layer in genome.m_NeuralNetwork.m_HiddenLayers)
            {
                MutateLayer(layer);
            }
            MutateLayer(genome.m_NeuralNetwork.m_OutputLayer);
        }

        public void MutateLayer(Layer layer)
        {
            foreach (Neuron neuron in layer.m_Neurons)
            {
                MutateNeuron(neuron);
            }
        }

        public void MutateNeuron(Neuron neuron)
        {
            for (int i = 0; i < neuron.m_Weights.Count; i++)
            {
                if (MyRandom.Chance(0.02f))
                {
                    neuron.m_Weights[i] += (MyRandom.NextFloat() - 0.5f) * 0.3f;
                }
                if (MyRandom.Chance(0.005f))
                {
                    neuron.m_Weights[i] += (MyRandom.NextFloat() - 0.5f) * 2.3f;
                }
            }
        }

        public Generation NextGeneration()
        {
            Evaluate();
            m_Population.Sort();
            float sum = 0.0f;
            foreach (Genome genome in m_Population)
            {
                sum += genome.m_Fitness;
            }
            sum /= m_Population.Count;
            Debug.Log(sum);
            Generation nextGeneration = new Generation(this);
            nextGeneration.KillLast();
            nextGeneration.Mutate();

            return nextGeneration;
        }
    }
}
