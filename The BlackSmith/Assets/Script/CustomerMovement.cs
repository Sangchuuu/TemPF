using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    Transform m_transMovPoint;
    float m_fMovSpeed;
    CustomerControl m_cusControl = CustomerControl.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        m_transMovPoint = m_cusControl.GetMovPoint();
        m_fMovSpeed = m_cusControl.GetMovSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Move(m_transMovPoint, true);
      
    }
    private void Move(Transform _movPoint, bool _isReturn)
    {
        float fDist = Vector3.Distance(this.transform.position, _movPoint.position);
        Vector3 vDIr = (_movPoint.position - this.transform.position).normalized;
        if (fDist > 0.1f)
        {
            this.gameObject.transform.position += vDIr * m_fMovSpeed * Time.deltaTime;
            if (m_transMovPoint == m_cusControl.GetResPoint())
            {
                Destroy(this.gameObject);
                m_cusControl.GetQueue().Dequeue();
            }
        }
        else
            m_transMovPoint = m_cusControl.GetResPoint();
        
    }
}
