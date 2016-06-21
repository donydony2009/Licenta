using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AI.NeuralNetworks
{
    class Activations
    {
        static public float Sigmoid(float a)
        {
            return (1 / (1 + (float)Math.Exp(-a)));
        }
    }
}
