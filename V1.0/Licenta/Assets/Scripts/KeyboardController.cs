using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {
    Vector3 m_Movement = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += GameConstants.PLAYER_SPEED * Time.deltaTime * m_Movement;
	}

    void FixedUpdate()
    {
        m_Movement = Vector3.zero;
        if(Input.GetKey(KeyCode.A))
        {
            m_Movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Movement.x += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            m_Movement.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Movement.z -= 1;
        }
        
        
    }
}
