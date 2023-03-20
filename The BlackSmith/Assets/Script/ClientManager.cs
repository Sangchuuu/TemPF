using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    private static ClientManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }

    public static ClientManager Instance 
    {
        get
        {
            if (instance == null)
                instance = new ClientManager();
            return instance;
        }
         
    }
    [SerializeField]
    float m_fmoveSpeed = 3f;
    [SerializeField]
    float m_ftime = 3f;
    [SerializeField]
    private Transform m_transResPoint;
    [SerializeField]
    private Transform m_transArrive;
    [SerializeField]
    private GameObject[] m_objClient = new GameObject[3];
    [SerializeField]
    private GameObject m_objSpecialClient;

    public float getMoveSpeed() { return m_fmoveSpeed; }
    public Transform getArrive() { return m_transArrive; }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        m_ftime -= Time.deltaTime;
        if (m_ftime <= 0)
        {
            GameObject objClient = null;
            int rand = Random.Range(0, m_objClient.Length);
            switch (rand)
            {
                case 0:
                  objClient = Instantiate(m_objClient[0]);
                    break;
                case 1:
                  objClient = Instantiate(m_objClient[1]);
                    break;
                case 2:
                  objClient = Instantiate(m_objClient[2]);
                    break;
            }
            objClient.transform.position = m_transResPoint.position;
            m_ftime = 3f;
        }
    }
}
