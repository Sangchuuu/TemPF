using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{

    // �������ΰ� �� �ҷ�����
    //public RectTransform timingbar;
    //public RectTransform bar;
    //public RectTransform[] coolTrans;
    //public RectTransform[] perpectTrans;
    public GameObject createState;
    public GameObject timingBar;
    public GameObject bar;
    public GameObject coolObjPrefab;
    public GameObject perfectObjPrefab;
    public GameObject[] coolObj;
    public GameObject[] perfectObj;

    // �������� �¿����
    public float leftlimit;
    public float rightlimit;

    bool moveRight = true;

    // �������� ����
    public float coolSize;
    public float perfectSize;

    private void Awake()
    {
        createState = this.gameObject;
        coolObj = new GameObject[3];
        perfectObj = new GameObject[3];
    }

    void Start()
    {        
        LimitSet();
        JudgmentSet();
        GameManager.instance.CreateJudgmentRange();
    }

    void Update()
    {
        Move();
        JudgeObjSet();
    }

    void Move()
    {
        if (moveRight)
        {
            if (bar.transform.position.x > rightlimit) moveRight = false;
            bar.transform.position += Vector3.right * GameManager.instance.movespeed * Time.deltaTime;
        }
        else
        {
            if (bar.transform.position.x < leftlimit) moveRight = true;
            bar.transform.position += Vector3.left * GameManager.instance.movespeed * Time.deltaTime;
        }
        GameManager.instance.barLocation = bar.transform.position.x;
    }

    // �������� �¿���� ����
    void LimitSet()
    {
        leftlimit = -(timingBar.transform.localScale.x / 2);
        rightlimit = timingBar.transform.localScale.x / 2;
    }

    // �������� ����
    void JudgmentSet()
    {
        coolSize = timingBar.transform.localScale.x * 0.12f;
        perfectSize = timingBar.transform.localScale.x * 0.06f;
        coolObjPrefab.transform.localScale = new Vector3(coolSize, timingBar.transform.localScale.y, 1f);
        perfectObjPrefab.transform.localScale = new Vector3(perfectSize, timingBar.transform.localScale.y, 1f);
        GameManager.instance.judgmentRange = new Vector2(leftlimit + (coolSize / 2), rightlimit - (coolSize / 2));
        GameManager.instance.coolSize = coolSize;
        GameManager.instance.perfectSize = perfectSize;
    }

    public void JudgeObjSet()
    {
        if (GameManager.instance.createJudge)
        {
            for (int i = 0; i < GameManager.instance.judgment; i++)
            {
                coolObj[i] = Instantiate(coolObjPrefab, createState.transform);
                perfectObj[i] = Instantiate(perfectObjPrefab, createState.transform);
                coolObj[i].transform.position = new Vector3(GameManager.instance.judgmentLocation[i], timingBar.transform.position.y, 0f);
                perfectObj[i].transform.position = new Vector3(GameManager.instance.judgmentLocation[i], timingBar.transform.position.y, 0f);                
            }
            GameManager.instance.createJudge = false;
        }
    }

}
