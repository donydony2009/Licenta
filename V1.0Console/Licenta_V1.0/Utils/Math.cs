﻿using System;
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

    static public float Distance(float x1, float y1, float x2, float y2)
    {
        return (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    }
}
