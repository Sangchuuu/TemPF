using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerControl : MonoBehaviour
{
    private static CustomerControl instance = null;
    [SerializeField]
    bool m_bResSwitch;
    [SerializeField]
    bool m_bTestSwitch;
    [SerializeField]
    float m_fTestTime;
    [SerializeField]
    float m_fMoveSpeed;
    [SerializeField]
    float m_fResTime;
    [SerializeField]
    Transform m_transMovPoint;
    [SerializeField]
    Transform m_transResPoint;
    [SerializeField]
    GameObject[] m_prefabCustomer;
    [SerializeField]
    GameObject m_objCusNow = null;
    [SerializeField]
    Queue m_queWaitList = new Queue();
    [SerializeField]
    GameObject[] m_objWaitinglist = new GameObject[10];
    [SerializeField]
    Dictionary<GameObject, string> m_dicWapon = new Dictionary<GameObject, string>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        DontDestroyOnLoad(instance);
    }

    public static CustomerControl GetInstance() { return instance; }
    public Transform GetMovPoint() { return m_transMovPoint; }
    public Transform GetResPoint() { return m_transResPoint; }
    public float GetMovSpeed() { return m_fMoveSpeed; }
    public Queue GetQueue() { return m_queWaitList; }
    

    void Start()
    {
        //m_dicWapon.Add(m_prefabCustomer[0], "Wood-dagger");
        //m_dicWapon.Add(m_prefabCustomer[0], "Rubber-dagger");
        //m_dicWapon.Add(m_prefabCustomer[0], "Iron-dagger");
        //m_dicWapon.Add(m_prefabCustomer[0], "Unique-dagger");
        m_bResSwitch = true;
        m_bTestSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_queWaitList.Count);
        if (m_bResSwitch == true)
        {
            StartCoroutine(Respwan());
        }
        if ( m_queWaitList.Count <= 10)
        {
            for (int i = 0; i < m_queWaitList.Count; i++)
            {
                m_objWaitinglist[i].SetActive(true);
            }
            for (int i = 9; i > m_queWaitList.Count; i--)
            {
                m_objWaitinglist[i].SetActive(false);
            }
        }
        else
            Debug.Log("Gameover");
        if (m_bTestSwitch == true)
        {
            StartCoroutine(RRRR());
        }
    }
    IEnumerator RRRR()
    {
        m_bTestSwitch = false;
        yield return new WaitForSeconds(m_fTestTime);
        TestFunc();
    }
    void TestFunc()
    {
        Debug.Log("DeQue");
        m_queWaitList.Dequeue();
        m_bTestSwitch = true;
    }
    IEnumerator Respwan()
    {
        m_bResSwitch = false;
        yield return new WaitForSeconds(m_fResTime);
        CusRespawn();
    }
    void CusRespawn()
    {
        int rand = Random.Range(0, 2);
        m_queWaitList.Enqueue(m_prefabCustomer[rand]);
        if (m_objCusNow == null)
        {
            m_objCusNow = Instantiate(m_queWaitList.Dequeue() as GameObject);
            m_objCusNow.transform.position = m_transResPoint.position;
        }
        m_bResSwitch = true;
    }
}
