using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace AI
{
    namespace NeuralNetworks
    {
        class NeuralNet : ICloneable
        {
            public Layer m_InputLayer = new Layer(MyMath.Sigmoid);
            public Layer m_OutputLayer = new Layer(MyMath.Identity);
            public List<Layer> m_HiddenLayers = new List<Layer>();

            public NeuralNet() { }
            public NeuralNet(List<int> configuration) { Randomize(configuration); }

            public NeuralNet(NeuralNet net) 
            {
                m_InputLayer = (Layer)net.m_InputLayer.Clone();
                m_OutputLayer = (Layer)net.m_OutputLayer.Clone();
                m_HiddenLayers = net.m_HiddenLayers.Clone();
            }

            public object Clone()
            {
                return new NeuralNet(this);
            }

            public static implicit operator JSONNode(NeuralNet net)
            {
                JSONClass json = new JSONClass();
                JSONArray hiddenLayers = new JSONArray();
                foreach (var layer in net.m_HiddenLayers)
                {
                    hiddenLayers.Add(layer);
                }
                json["InputLayer"] = net.m_InputLayer;
                json["OutputLayer"] = net.m_OutputLayer;
                json["HiddenLayers"] = hiddenLayers;
                return json;
            }

            public List<float> Evaluate(List<float> input)
            {
                //List<float> output; = m_InputLayer.Evaluate(input);
                List<float> output = input;
                foreach (Layer hiddenLayer in m_HiddenLayers)
                {
                    output = hiddenLayer.Evaluate(output);
                }
                return m_OutputLayer.Evaluate(output);
            }

            //Split into configure and randomize
            public void Randomize(List<int> configuration)
            {
                if (configuration.Count < 2)
                    return;
                m_InputLayer = new Layer(MyMath.Sigmoid, configuration[0], 0);
                m_HiddenLayers.Clear();
                for (int i = 1; i < configuration.Count - 1; i++)
                {
                    Layer hidden = new Layer(MyMath.Sigmoid, configuration[i], configuration[i - 1] + 1);
                    m_HiddenLayers.Add(hidden);
                }
                m_OutputLayer = new Layer(MyMath.Identity, configuration[configuration.Count - 1], configuration[configuration.Count - 2] + 1);
            }
        }
    }
}
