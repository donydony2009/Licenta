using UnityEngine;
using System.Collections;

public class StepController : MonoBehaviour {

    Vector3 m_Movement = Vector3.zero;
    public StepObject<float> m_StepObject = null;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (m_StepObject != null)
        {
            transform.position += GameConstants.PLAYER_SPEED * Time.deltaTime * m_Movement;
        }
	}

    void FixedUpdate()
    {
        if (m_StepObject != null)
        {
            float angle = m_StepObject.Step();
            m_Movement.x = Mathf.Cos(angle);
            m_Movement.z = Mathf.Sin(angle);
        }
    }
}
