using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MyRandom
{
    private static Random random = new Random();
    public static float NextFloat() { return (float)random.NextDouble(); }
    public static bool Chance(float trueChance) { return random.NextDouble() < trueChance; }
}

