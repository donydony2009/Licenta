using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace AI
{
    namespace NeuralNetworks
    {
        class Neuron : ICloneable
        {
            public List<float> m_Weights;

            public Neuron()
            {
                m_Weights = new List<float>();
            }

            public Neuron(Neuron neuron)
            {
                m_Weights = new List<float>(neuron.m_Weights);
            }

            public object Clone()
            {
                return new Neuron(this);
            }

            public static implicit operator JSONNode(Neuron neuron)
            {
                JSONClass json = new JSONClass();
                json["Weights"] = neuron.m_Weights;
                return json;
            }

            public void Randomize(int inputsNo)
            {
                m_Weights.Clear();
                for (int i = 0; i < inputsNo; i++)
                {
                    m_Weights.Add(5 * MyRandom.NextFloat() - 5 * MyRandom.NextFloat());
                }
            }
        }
    }
}
