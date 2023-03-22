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
    public List<Vector2> hitJudgment;
    public List<float> judgmentLocation;
    public Vector2 judgmentRange;
    public int currentJudgment;
    public int judgment = 2;
    public bool createJudge;
    public int count = 0;
    public float coolSize;
    public float perpectSize;
    public bool isHit;
    public bool isMiss;
    public int hitIndex;



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
        if (clickedBt1 && count != judgment)
        {
            if(currentJudgment > 0)
            {
                for (int index = 0; index < coolRange.Count; index++)
                {
                    if (barLocation < coolRange[index].y && barLocation > coolRange[index].x)
                    {
                        if (barLocation < perpectRange[index].y && barLocation > perpectRange[index].x)
                        {
                            //Perpect 판정일때
                            Debug.Log("Perpect!");
                            currentJudgment--;
                            hitJudgment.Add(coolRange[index]);
                            hitJudgment.Add(perpectRange[index]);
                            coolRange.RemoveAt(index);
                            perpectRange.RemoveAt(index);
                            hitIndex = index;
                            count++;
                            isHit = true;
                            break;
                        }
                        else
                        {
                            //Cool 판정일때
                            Debug.Log("Cool!");
                            currentJudgment--;
                            hitJudgment.Add(coolRange[index]);
                            hitJudgment.Add(perpectRange[index]);
                            coolRange.RemoveAt(index);
                            perpectRange.RemoveAt(index);
                            hitIndex = index;
                            count++;
                            isHit = true;
                            break;
                        }
                    }
                }
                if (isHit == false)
                {
                    //Miss 판정일때                
                    Debug.Log("Miss!");
                    if(hitJudgment != null)
                    {
                        if (count == 1)
                        {
                            coolRange.Add(hitJudgment[0]);
                            perpectRange.Add(hitJudgment[1]);

                        }
                        else if (count == 2)
                        {
                            coolRange.Add(hitJudgment[0]);
                            perpectRange.Add(hitJudgment[1]);
                            coolRange.Add(hitJudgment[2]);
                            perpectRange.Add(hitJudgment[3]);
                        }
                        hitJudgment.Clear();
                        currentJudgment = judgment;
                        isMiss = true;
                    }                    
                    count = 0;
                }
                clickedBt1 = false;
                isHit = false;
            }            
        }
        else
        {
            clickedBt1 = false;
        }
        if (clickedBt2)
        {
            if (currentJudgment <= 0)
            {
                Debug.Log("납품완료");
                coolRange.Clear();
                perpectRange.Clear();
                judgmentLocation.Clear();
                hitJudgment.Clear();
                // 다음 대기열의 물품에 대한 정보를 불러와 셋팅
                // judgment = 2;
                CreateJudgmentRange();
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

        currentJudgment = judgment;
        count = 0;
        createJudge = true;                
    }
}
