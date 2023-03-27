using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{
    // UI�� ���۽�
    //public RectTransform timingbar;
    //public RectTransform bar;
    //public RectTransform[] coolTrans;
    //public RectTransform[] perpectTrans;
    
    // �����ٿ� ������������ �� ������Ʈ ���
    public GameObject createState;
    public GameObject timingBar;
    public GameObject bar;
    public GameObject coolObjPrefab;
    public GameObject perfectObjPrefab;
    public GameObject specialObjPrefab;
    public GameObject[] coolObj;
    public GameObject[] perfectObj;
    public GameObject specialObj;

    // �������� �¿����
    public float leftlimit;
    public float rightlimit;

    // ������ �¿� ��ȯ üũ
    bool moveRight = true;

    // �������� ����    
    public float setCoolSize;
    public float setPerfectSize;
    public float setSpecialSize;
    [HideInInspector] public float coolSize;
    [HideInInspector] public float perfectSize;
    [HideInInspector] public float specialSize;

    private void Awake()
    {
        createState = this.gameObject;
        ParameterInit();        
    } 

    void Start()
    {        
        LimitSet();
        JudgmentSet();
        CreateJudgeObj();
        GameManager.instance.CreateJudgmentRange();
    }

    void Update()
    {
        Move();
        JudgeObjSet();
    }

    private void ParameterInit() // �ʱ�ȭ �Լ�
    {
        coolObj = new GameObject[3];
        perfectObj = new GameObject[3];
        setCoolSize = 15f;
        setPerfectSize = 6f;
    }

    void Move()
    {
        if (moveRight) // ���������� �����϶�
        {
            if (bar.transform.position.x > rightlimit) moveRight = false;
            bar.transform.position += Vector3.right * GameManager.instance.movespeed * Time.deltaTime;
        }
        else // �������� �����϶�
        {
            if (bar.transform.position.x < leftlimit) moveRight = true;
            bar.transform.position += Vector3.left * GameManager.instance.movespeed * Time.deltaTime;
        }
        GameManager.instance.barLocation = bar.transform.position.x;
    }
    
    void LimitSet() // ������ �̵� �¿���� ����
    {
        leftlimit = -(timingBar.transform.localScale.x / 2);
        rightlimit = timingBar.transform.localScale.x / 2;
    }
    
    void JudgmentSet() // �������� ���� ����
    {
        coolSize = timingBar.transform.localScale.x * (setCoolSize / 100);
        perfectSize = timingBar.transform.localScale.x * (setPerfectSize / 100);
        setSpecialSize = setPerfectSize;
        specialSize = timingBar.transform.localScale.x * (setSpecialSize / 100);
        coolObjPrefab.transform.localScale = new Vector3(coolSize, timingBar.transform.localScale.y, 1f);
        perfectObjPrefab.transform.localScale = new Vector3(perfectSize, timingBar.transform.localScale.y, 1f);
        specialObjPrefab.transform.localScale = new Vector3(specialSize, timingBar.transform.localScale.y, 1f);
        GameManager.instance.judgmentRange = new Vector2(leftlimit + (coolSize / 2), rightlimit - (coolSize / 2));
        GameManager.instance.coolSize = coolSize;
        GameManager.instance.perfectSize = perfectSize;
        GameManager.instance.specialSize = specialSize;
    }

    void CreateJudgeObj() // �������� ����
    {
        specialObj = Instantiate(specialObjPrefab, createState.transform);
        specialObj.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            coolObj[i] = Instantiate(coolObjPrefab, createState.transform);
            coolObj[i].SetActive(false);
            perfectObj[i] = Instantiate(perfectObjPrefab, createState.transform);
            perfectObj[i].SetActive(false);
        }
    }

    void JudgeObjSet() // ���������� ������ ��ġ�� �̵���Ŵ
    {
        if (GameManager.instance.createJudge)
        {
            if (GameManager.instance.specialNPC || GameManager.instance.isFeverTime)
            {                
                specialObj.transform.position = new Vector3(GameManager.instance.judgmentLocation[0], timingBar.transform.position.y, 0f);
                specialObj.SetActive(true);
            }
            else
            {
                for (int i = 0; i < GameManager.instance.judgment; i++)
                {
                    coolObj[i].transform.position = new Vector3(GameManager.instance.judgmentLocation[i], timingBar.transform.position.y, 0f);                    
                    perfectObj[i].transform.position = new Vector3(GameManager.instance.judgmentLocation[i], timingBar.transform.position.y, 0f);
                    coolObj[i].SetActive(true);
                    perfectObj[i].SetActive(true);
                }
            }            
            GameManager.instance.createJudge = false;
        }
    }

}
