using System;
using System.Collections.Generic;

public interface StepObject<T>
{
    void Initialize();
    List<float> GetInputs();
    T Step();
}

