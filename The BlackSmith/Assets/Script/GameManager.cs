using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool clickedBt1;
    public bool clickedBt2;    
    public float barLocation;
    public List<Vector2> coolLocation;
    public List<Vector2> perpectLocation;
    public Vector2 judgmentRange;
    public int judgment;
    public float coolSize;
    public float perpectSize;
    float coolRightRange;
    float coolLeftRange;    
    float perpectRightRange;
    float perpectleftRange;
    bool Waiting = true;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Waiting)
        {
            CreateJudgmentRange();
            Waiting = false;
        }
        UseButton();
    }

    void UseButton()
    {
        if (clickedBt1)
        {
            for(int index = 0; index < coolLocation.Count; index++)
            {
                coolLeftRange = coolLocation[index].x;
                coolRightRange = coolLocation[index].y;

                if (barLocation < coolRightRange && barLocation > coolLeftRange)
                {
                    if (barLocation < perpectRightRange && barLocation > perpectleftRange)
                    {
                        Debug.Log("Perpect!");
                    }
                    else
                    {
                        Debug.Log("Cool!");
                    }
                }
            }            
            clickedBt1 = false;
        }
        if (clickedBt2)
        {
            if (judgment == 0)
            {
                Debug.Log("납품완료");
            }
            else
            {
                Debug.Log("제작 미완료");
            }
            clickedBt2 = false;
        }
    }

    void CreateJudgmentRange()
    {
        float random1 = Random.Range(judgmentRange.x, judgmentRange.y);
        float random2 = Random.Range(judgmentRange.x, judgmentRange.y);
        float random3 = Random.Range(judgmentRange.x, judgmentRange.y);

        coolLocation.Add(new Vector2(random1 - (coolSize / 2), random1 + (coolSize / 2)));
        coolLocation.Add(new Vector2(random2 - (coolSize / 2), random2 + (coolSize / 2)));
        coolLocation.Add(new Vector2(random3 - (coolSize / 2), random3 + (coolSize / 2)));

        perpectLocation.Add(new Vector2(random1 - (perpectSize / 2), random1 + (perpectSize / 2)));
        perpectLocation.Add(new Vector2(random2 - (perpectSize / 2), random2 + (perpectSize / 2)));
        perpectLocation.Add(new Vector2(random3 - (perpectSize / 2), random3 + (perpectSize / 2)));
    }
}
