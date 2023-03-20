using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{
    public RectTransform timingbar;
    public RectTransform bar;
    public float leftlimit;
    public float rightlimit;
    public float movespeed = 300f;
    bool moveRight = true;
        

    void Start()
    {
        limitset();
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
    }

    void limitset()
    {
        leftlimit = 0 - (timingbar.sizeDelta.x / 2);
        rightlimit = timingbar.sizeDelta.x / 2;
    }
}
