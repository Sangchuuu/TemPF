using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{

    // 판정라인과 바 불러오기
    public RectTransform timingbar;
    public RectTransform bar;
    public RectTransform[] coolTrans;
    public RectTransform[] perpectTrans;
    public GameObject[] coolObj;
    public GameObject[] perpectObj;

    // 판정라인 좌우범위
    public float leftlimit;
    public float rightlimit;

    bool moveRight = true;

    // 판정범위 설정
    public float coolSize = 200f;
    public float perpectSize = 80f;     

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
            if (bar.anchoredPosition.x > rightlimit) moveRight = false;
            bar.anchoredPosition += Vector2.right * GameManager.instance.movespeed * Time.deltaTime;            
        }
        else
        {
            if (bar.anchoredPosition.x < leftlimit) moveRight = true;
            bar.anchoredPosition += Vector2.left * GameManager.instance.movespeed * Time.deltaTime;
        }
        GameManager.instance.barLocation = bar.anchoredPosition.x;
    }

    // 판정라인 좌우범위 설정
    void LimitSet()
    {
        leftlimit = 0 - (timingbar.sizeDelta.x / 2);
        rightlimit = timingbar.sizeDelta.x / 2;
    }

    // 판정범위 설정
    void JudgmentSet()
    {
        GameManager.instance.judgmentRange = new Vector2(leftlimit + (coolSize/2), rightlimit - (coolSize/2));
        GameManager.instance.coolSize = coolSize;
        GameManager.instance.perpectSize = perpectSize;
    }

    public void JudgeObjSet()
    {
        if(GameManager.instance.createJudge)
        {
            for (int i = 0; i < GameManager.instance.judgment; i++)
            {
                coolTrans[i].anchoredPosition = new Vector2(GameManager.instance.judgmentLocation[i], 0f);
                perpectTrans[i].anchoredPosition = new Vector2(GameManager.instance.judgmentLocation[i], 0f);
                coolObj[i].SetActive(true);
                perpectObj[i].SetActive(true);
            }
            GameManager.instance.createJudge = false;
        }
    }

}
