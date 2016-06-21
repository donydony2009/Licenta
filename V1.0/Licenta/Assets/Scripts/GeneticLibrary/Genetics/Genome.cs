using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AI.NeuralNetworks;

namespace AI.Genetics
{
    class Genome : Evaluable, IComparable<Genome>, ICloneable
    {
        public Genome(List<int> config)
        {
            m_NeuralNetwork = new NeuralNet(config);
            m_Fitness = 0;
        }

        public Genome(NeuralNet net)
        {
            m_NeuralNetwork = (NeuralNet)net.Clone();
            m_Fitness = 0;
        }

        public object Clone()
        {
            return new Genome(m_NeuralNetwork);
        }

        public List<float> Evaluate(List<float> inputs)
        {
            return m_NeuralNetwork.Evaluate(inputs);
        }

        public static bool operator <(Genome a, Genome b)
        {
            return (a.m_Fitness < b.m_Fitness);
        }

        public static bool operator <=(Genome a, Genome b)
        {
            return (a.m_Fitness < b.m_Fitness);
        }

        public static bool operator >(Genome a, Genome b)
        {
            return (a.m_Fitness > b.m_Fitness);
        }

        public static bool operator >=(Genome a, Genome b)
        {
            return (a.m_Fitness >= b.m_Fitness);
        }

        public int CompareTo(Genome compare)
        {
            return m_Fitness.CompareTo(compare.m_Fitness);
        }

        public string m_Id = System.Guid.NewGuid().ToString();
        public float m_Fitness;
        public NeuralNet m_NeuralNetwork;
    }
}
