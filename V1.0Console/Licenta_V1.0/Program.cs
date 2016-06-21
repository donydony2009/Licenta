using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta_V1._0
{
    class Program
    {
        static float FittnessSum(AI.Genetics.Evaluable eval)
        {
            List<float> input = new List<float>();
            float error = 0.0f;
            for(int i = 0; i < 10; i++)
            {
                input.Clear();
                float a = MyRandom.NextFloat() * 5;
                float b = MyRandom.NextFloat() * 5;
                input.Add(a);
                input.Add(b);
                float myResult = eval.Evaluate(input)[0];
                error += Math.Abs(myResult - (a + b));
            }
            return error;
        }

        static float FittnessDiff(AI.Genetics.Evaluable eval)
        {
            List<float> input = new List<float>();
            float error = 0.0f;
            for (int i = 0; i < 10; i++)
            {
                input.Clear();
                float a = MyRandom.NextFloat() * 5;
                float b = MyRandom.NextFloat() * 5;
                input.Add(a);
                input.Add(b);
                float myResult = eval.Evaluate(input)[0];
                error += Math.Abs(myResult - (a - b));
            }
            return error;
        }

        static float FittnessGoTo(AI.Genetics.Evaluable eval)
        {
            List<float> input = new List<float>();
            float error = 0.0f;
            for (int i = 0; i < 100; i++)
            {
                input.Clear();
                float x1 = MyRandom.NextFloat();
                float y1 = MyRandom.NextFloat();
                float x2 = MyRandom.NextFloat();
                float y2 = MyRandom.NextFloat();
                input.Add(x1);
                input.Add(y1);
                input.Add(x2);
                input.Add(y2);
                float dirX = x2 - x1;
                float dirY = y2 - y1;
                float tan = (float)Math.Atan(dirY/dirX );
                if (dirX < 0.0f)
                {
                    if (dirY > 0.0f)
                    {
                        tan += (float)Math.PI;
                    }
                    else
                    {
                        tan -= (float)Math.PI;
                    }
                }
                float myResult = MyMath.ClampAngle(eval.Evaluate(input)[0]);

                error += MyMath.AngleDifference(tan, myResult) ;
            }
            return error;
        }

        static void Main(string[] args)
        {
            SimpleJSON.JSONNode json;
            List<int> config = new List<int>();
            config.Add(4);
            config.Add(10);
            config.Add(10);
            config.Add(10);
            config.Add(1);
            AI.NeuralNetworks.NeuralNet net = new AI.NeuralNetworks.NeuralNet(config);
            json = net;
            List<float> input = new List<float>();
            input.Add(3);
            input.Add(3);
            input.Add(5);
            input.Add(5);
            var result = net.Evaluate(input);
            float dirX = -1;
            float dirY = 1;
            float tan = (float)Math.Atan(dirY / dirX);
            AI.Genetics.Generation generation = new AI.Genetics.Generation(1000, 500, config, FittnessGoTo);
            generation.NextGeneration();
            foreach (var f in result)
            {
                Console.Write(f + " ");
            }
            Console.WriteLine("");
            Console.Read();
        }
    }
}
