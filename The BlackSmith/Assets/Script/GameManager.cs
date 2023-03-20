using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool clickedBt1;
    public bool clickedBt2;    
    public float barLocation;
    public List<Vector2> coolRange;
    public List<Vector2> perpectRange;
    public List<float> judgmentLocation;
    public Vector2 judgmentRange;
    public int judgment = 2;
    public bool createJudge;
    public int count = 0;
    public float coolSize;
    public float perpectSize;
    

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UseButton();        
    }

    void UseButton()
    {
        if (clickedBt1)
        {
            for(int index = 0; index < coolRange.Count; index++)
            {
                if (barLocation < coolRange[index].y && barLocation > coolRange[index].x)
                {
                    if (barLocation < perpectRange[index].y && barLocation > perpectRange[index].x)
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
                coolRange.Clear();
                perpectRange.Clear();
                CreateJudgmentRange();
                count = 0;
            }
            else
            {
                Debug.Log("제작 미완료");
            }
            clickedBt2 = false;
        }
    }

    public void CreateJudgmentRange()
    {
        float random1 = Random.Range(judgmentRange.x, judgmentRange.y);
        float random2 = Random.Range(judgmentRange.x, judgmentRange.y);
        float random3 = Random.Range(judgmentRange.x, judgmentRange.y);

        coolRange.Add(new Vector2(random1 - (coolSize / 2), random1 + (coolSize / 2)));
        coolRange.Add(new Vector2(random2 - (coolSize / 2), random2 + (coolSize / 2)));
        coolRange.Add(new Vector2(random3 - (coolSize / 2), random3 + (coolSize / 2)));
        
        perpectRange.Add(new Vector2(random1 - (perpectSize / 2), random1 + (perpectSize / 2)));
        perpectRange.Add(new Vector2(random2 - (perpectSize / 2), random2 + (perpectSize / 2)));
        perpectRange.Add(new Vector2(random3 - (perpectSize / 2), random3 + (perpectSize / 2)));

        judgmentLocation.Add(random1);
        judgmentLocation.Add(random2);
        judgmentLocation.Add(random3);
        
        createJudge = true;                
    }
}
