using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MyMath
{
    static public float Identity(float a)
    {
        return a;
    }

    static public float Sigmoid(float a)
    {
        return (1 / (1 + (float)Math.Exp(-a)));
    }

    static public float AngleDifference(float angle1, float angle2)
    {
        return Math.Min(Math.Abs(angle1 - angle2), 360 - Math.Abs(angle1) - Math.Abs(angle2));
    }

    static public float ClampAngle(float angle)
    {
        while (angle < Math.PI)
        {
            angle += (float)Math.PI * 2;
        }

        while (angle > Math.PI)
        {
            angle -= (float)Math.PI * 2;
        }

        return angle;
    }
}
