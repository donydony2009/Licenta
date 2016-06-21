using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetics
{
    interface Evaluable
    {
        List<float> Evaluate(List<float> input);
    }
}
