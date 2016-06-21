using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {
    public float m_MaxX;
    public float m_MaxZ;
    public float m_MinX;
    public float m_MinZ;
    float lastTime = 0.0f;

	// Use this for initialization
	void Start () {
        RandomPosition();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void RandomPosition()
    {
        transform.position = new Vector3(Random.Range(m_MinX, m_MaxX), transform.position.y, Random.Range(m_MinX, m_MaxX));
    }
}
