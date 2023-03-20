using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{
    // �������ΰ� �� �ҷ�����
    public RectTransform timingbar;
    public RectTransform bar;

    // �������� �¿����
    public float leftlimit;
    public float rightlimit;

    // ������ ���ǵ�, �¿� ������ �μ�
    public float movespeed = 300f;
    bool moveRight = true;

    // �������� ����
    public float coolSize = 200f;
    public float perpectSize = 80f;     

    void Start()
    {
        LimitSet();
        JudgmentSet();
    }

    void Update()
    {        
        Move();
    }

    void Move()
    {
        if (moveRight)
        {
            if (bar.anchoredPosition.x > rightlimit) moveRight = false;
            bar.anchoredPosition += Vector2.right * movespeed * Time.deltaTime;            
        }
        else
        {
            if (bar.anchoredPosition.x < leftlimit) moveRight = true;
            bar.anchoredPosition += Vector2.left * movespeed * Time.deltaTime;
        }
        GameManager.instance.barLocation = bar.anchoredPosition.x;
    }

    // �������� �¿���� ����
    void LimitSet()
    {
        leftlimit = 0 - (timingbar.sizeDelta.x / 2);
        rightlimit = timingbar.sizeDelta.x / 2;
    }

    // �������� ����
    void JudgmentSet()
    {
        GameManager.instance.judgmentRange = new Vector2(leftlimit + coolSize, rightlimit - coolSize);
        GameManager.instance.coolSize = coolSize;
        GameManager.instance.perpectSize = perpectSize;
    }



}
