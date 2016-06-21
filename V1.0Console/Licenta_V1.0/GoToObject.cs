using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GoToObject : StepObject<float>
{
    List<float> input = new List<float>();
    AI.Genetics.Evaluable evaluable;
    public GoToObject(AI.Genetics.Evaluable eval)
    {
        evaluable = eval;
        Initialize();
    }
    public void Initialize()
    {
        input.Add(MyRandom.NextFloat());
        input.Add(MyRandom.NextFloat());
        input.Add(MyRandom.NextFloat());
        input.Add(MyRandom.NextFloat());
    }

    public List<float> GetInputs()
    {
        return input;
    }

    public float Step()
    {
        float result = evaluable.Evaluate(input)[0];
        result = MyMath.ClampAngle(result);
        input[0] += (float)Math.Cos(result) * 0.1f * 1;
        input[1] += (float)Math.Sin(result) * 0.1f * 1;
        if (input[0] > 1.0f)
            input[0] = 1.0f;
        if (input[0] < 0.0f)
            input[0] = 0.0f;
        if (input[1] > 1.0f)
            input[1] = 1.0f;
        if (input[1] < 0.0f)
            input[1] = 0.0f;
        if (MyMath.Distance(input[0], input[1], input[2], input[3]) < 0.05f)
        {
            input[2] = MyRandom.NextFloat();
            input[3] = MyRandom.NextFloat();
        }
        return result;
    }


}
