using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    bool m_isRun;
    float m_rate,m_x,m_nextX,m_y,m_z;
    public float moveSpeed;

    private void Start()
    {
        m_y = transform.position.y;
        m_z = transform.position.z;
    }
    private void Update()
    {
        if (m_isRun)
        {
            m_x = transform.position.x;
            m_rate = Time.deltaTime * moveSpeed;
            if (Mathf.Abs(m_nextX - m_x) <= 0.05f)
            {
                m_isRun = false;
                transform.position = new Vector3(m_nextX, m_y, m_z);
            }
            else
            {
                transform.position = new Vector3(Mathf.Lerp(m_x, m_nextX, m_rate), m_y, m_z);
            }
        }
    }
    public void Run(float distance)
    {
        
        m_nextX = transform.position.x + distance;
        m_isRun = true;
    }
}
