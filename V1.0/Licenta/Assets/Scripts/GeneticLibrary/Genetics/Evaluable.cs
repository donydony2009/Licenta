using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.Genetics
{
    interface Evaluable
    {
        List<float> Evaluate(List<float> input);
    }
}
