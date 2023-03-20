using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Transform m_tranArrive;
    float m_fspeed;
    // Start is called before the first frame update
    void Start()
    {
        m_fspeed = ClientManager.Instance.getMoveSpeed();
        m_tranArrive = ClientManager.Instance.getArrive();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * m_fspeed * Time.deltaTime;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if(collision.CompareTag(m_tranArrive.tag))
        {
            m_fspeed = 0;
        }
    }
}
