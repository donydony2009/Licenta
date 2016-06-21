using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.Genetics{
    public class EvaluableStepObject : StepObject<float> {
        public Transform m_MyTransform;
        public Transform m_TargetTransform;
        public Evaluable m_Evaluable;

        public EvaluableStepObject(Transform myTransform, Transform targetTransform, Evaluable evaluable)
        {
            m_MyTransform = myTransform;
            m_TargetTransform = targetTransform;
            m_Evaluable = evaluable;
        }

        float Step()
        {
            List<float> input = new List<float>();
            input.Add(m_MyTransform.position.x);
            input.Add(m_MyTransform.position.z);
            input.Add(m_TargetTransform.position.x);
            input.Add(m_TargetTransform.position.z);
            return m_Evaluable.Evaluate(input)[0];
        }
    }
}
