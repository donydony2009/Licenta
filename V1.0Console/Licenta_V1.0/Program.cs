using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta_V1._0
{
    class Program
    {
        public delegate float FitnessFunctor<T>(List<float> inputs, T eval);
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

        static float FittnessGoTo(List<float> input, float output)
        {
            float x1 = input[0];
            float y1 = input[1];
            float x2 = input[2];
            float y2 = input[3];
            float dirX = x2 - x1;
            float dirY = y2 - y1;
            float tan = (float)Math.Atan(dirY / dirX);
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
            float myResult = MyMath.ClampAngle(output);

            return MyMath.AngleDifference(tan, myResult);
        }

        static float FittnessGoToDistance(List<float> input, float output)
        {
            float x1 = input[0];
            float y1 = input[1];
            float x2 = input[2];
            float y2 = input[3];

            if (MyMath.Distance(input[0], input[1], input[2], input[3]) < 0.05f)
            {
                return -300;
            }

            return MyMath.Distance(x1, y1, x2, y2) * MyMath.Distance(x1, y1, x2, y2);
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

                error += (MyMath.AngleDifference(tan, myResult) * 6) * (MyMath.AngleDifference(tan, myResult) * 6);
            }
            return error;
        }

        static float FittnessGoTo2(AI.Genetics.Evaluable eval)
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
                float tan = (float)Math.Atan2(dirY, dirX);
                var result = eval.Evaluate(input);
                float myResult = (float)Math.Atan2(result[1], result[0]);

                error += (MyMath.AngleDifference(tan, myResult) * 6) * (MyMath.AngleDifference(tan, myResult) * 6);
            }
            return error;
        }

        static float StepFitness<T>(FitnessFunctor<T> fitnessFunctor, StepObject<T> stepObject, int numberOfSteps)
        {
            float error = 0.0f;
            for (int i = 0; i < numberOfSteps; i++)
            {
                error += fitnessFunctor(stepObject.GetInputs(), stepObject.Step());
            }
            return error;
        }

        static float StepGoToFitness(AI.Genetics.Evaluable eval)
        {
            GoToObject2 obj = new GoToObject2(eval);
            return StepFitness<float>(FittnessGoToDistance, obj, 1000);
        }

        static void Main(string[] args)
        {
            SimpleJSON.JSONNode json;
            List<int> config = new List<int>();
            config.Add(4);
            config.Add(5);
            config.Add(5);
            config.Add(10);
            config.Add(2);
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
            AI.Genetics.Generation generation = new AI.Genetics.Generation(1000, 800, config, StepGoToFitness);
            generation.NextGeneration();
            generation.ChangeFitnessFunctor(StepGoToFitness);
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
