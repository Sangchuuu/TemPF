using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    Transform m_transMovPoint;
    bool m_bArrived;
    float m_fMovSpeed;
    CustomerControl m_cusControl = CustomerControl.GetInstance();
    // Start is called before the first frame update
    void Start()
    {
        m_bArrived = false;
        m_transMovPoint = m_cusControl.GetMovPoint();
        m_fMovSpeed = m_cusControl.GetMovSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Move(m_transMovPoint);
    }
    private void Move(Transform _movPoint)
    {
        float fDist = Vector3.Distance(this.transform.position, _movPoint.position);
        Vector3 vDIr = (_movPoint.position - this.transform.position).normalized;
        Debug.Log(fDist);
        Debug.Log(vDIr);
        if (fDist > 0.1f)
            this.gameObject.transform.position += vDIr * m_fMovSpeed * Time.deltaTime;
        else
            m_transMovPoint = m_cusControl.GetResPoint();
    }
}
