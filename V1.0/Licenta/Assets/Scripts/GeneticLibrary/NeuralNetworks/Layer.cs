using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace AI
{
    namespace NeuralNetworks
    {
        class Layer : ICloneable
        {
            const float BIAS = 1.0f;
            public List<Neuron> m_Neurons = new List<Neuron>();
            public delegate float ActivationFunction(float activation);
            ActivationFunction m_ActivationFunction;

            public Layer(ActivationFunction activation) { m_ActivationFunction = activation; }
            public Layer(ActivationFunction activation, int neuronsNo, int inputsNo) { m_ActivationFunction = activation; Randomize(neuronsNo, inputsNo); }

            public Layer(Layer layer) 
            {
                m_Neurons = layer.m_Neurons.Clone();
                m_ActivationFunction = layer.m_ActivationFunction;
            }

            public object Clone()
            {
                return new Layer(this);
            }

            public List<float> Evaluate(List<float> input)
            {
                List<float> output = new List<float>();

		        foreach(Neuron neuron in m_Neurons)
		        {
			        float activation = 0.0f;

                    for (int j = 0; j < neuron.m_Weights.Count - 1; j++)
			        {
                        activation += input[j] * neuron.m_Weights[j];
			        }

                    activation += neuron.m_Weights[neuron.m_Weights.Count - 1] * BIAS;

			        output.Add(m_ActivationFunction(activation));
		        }
                return output;
            }

            public static implicit operator JSONNode(Layer layer)
            {
                JSONClass json = new JSONClass();
                JSONArray neurons = new JSONArray();
                foreach (var neuron in layer.m_Neurons)
                {
                    neurons.Add(neuron);
                }
                json["Neurons"] = neurons;
                return json;
            }

            public void Randomize(int neuronsNo, int inputsNo)
            {
                m_Neurons.Clear();
                for (int i = 0; i < neuronsNo; i++)
                {
                    Neuron neuron = new Neuron();
                    neuron.Randomize(inputsNo);
                    m_Neurons.Add(neuron);
                }
            }
        }
    }
}
