using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public BarControl barControl;

    public void HitButton()
    {
        if (GameManager.instance.currentJudgment > 0)
        {
            // 특수 손님 일때
            if (GameManager.instance.specialNPC || GameManager.instance.isFeverTime || GameManager.instance.feverIsOver)
            {
                if (GameManager.instance.barLocation < GameManager.instance.specialRange.y && GameManager.instance.barLocation > GameManager.instance.specialRange.x)
                {
                    Debug.Log("Special!");
                    // 맞추면 피버 발동
                    GameManager.instance.isSpecial = true;
                    if (GameManager.instance.specialNPC) GameManager.instance.specialNPC = false;
                    if (!GameManager.instance.feverIsOver) GameManager.instance.isFeverTime = true;                                                            
                    GameManager.instance.currentJudgment--;
                    GameManager.instance.specialRange = Vector2.zero;
                    Destroy(barControl.specialObj);
                    if(!GameManager.instance.isFeverTime && !GameManager.instance.specialNPC)
                    {
                        GameManager.instance.feverIsOver = false;
                    }
                }
                if(!GameManager.instance.isSpecial)
                {
                    Debug.Log("Miss");
                    GameManager.instance.specialNPC = false;                    
                    if (!GameManager.instance.isFeverTime)
                    {
                        GameManager.instance.feverIsOver = true;
                        barControl.specialObj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    }
                }
            }
            // 피버가 아닐때
            else
            {
                for (int index = 0; index < GameManager.instance.judgment; index++)
                {
                    if (GameManager.instance.barLocation < GameManager.instance.perpectRange[index].y && GameManager.instance.barLocation > GameManager.instance.perpectRange[index].x)
                    {
                        //Perpect 판정일때
                        Debug.Log("Perpect!");
                        GameManager.instance.isPerfect = true;
                        GameManager.instance.currentJudgment--;
                        GameManager.instance.coolRange[index] = Vector2.zero;
                        GameManager.instance.perpectRange[index] = Vector2.zero;
                        Destroy(barControl.coolObj[index]);
                        Destroy(barControl.perfectObj[index]);
                        GameManager.instance.hitIndex[index] = true;
                        break;
                    }
                }
                if (!GameManager.instance.isPerfect)
                {
                    for (int index = 0; index < GameManager.instance.judgment; index++)
                    {
                        if (GameManager.instance.barLocation < GameManager.instance.coolRange[index].y && GameManager.instance.barLocation > GameManager.instance.coolRange[index].x)
                        {
                            //Cool 판정일때
                            Debug.Log("Cool!");
                            GameManager.instance.isCool = true;
                            GameManager.instance.currentJudgment--;
                            GameManager.instance.coolRange[index] = Vector2.zero;
                            GameManager.instance.perpectRange[index] = Vector2.zero;
                            Destroy(barControl.coolObj[index]);
                            Destroy(barControl.perfectObj[index]);
                            GameManager.instance.hitIndex[index] = true;
                            break;
                        }
                    }
                }
                if (!GameManager.instance.isPerfect && !GameManager.instance.isCool)
                {
                    //Miss 판정일때
                    Debug.Log("Miss!");
                    if (GameManager.instance.currentJudgment != GameManager.instance.judgment)
                    {
                        for (int i = 0; i < GameManager.instance.judgment; i++)
                        {
                            if (GameManager.instance.hitIndex[i])
                            {
                                GameManager.instance.coolRange[i] = new Vector2(GameManager.instance.judgmentLocation[i] - (GameManager.instance.coolSize / 2),
                                    GameManager.instance.judgmentLocation[i] + (GameManager.instance.coolSize / 2));
                                GameManager.instance.perpectRange[i] = new Vector2(GameManager.instance.judgmentLocation[i] - (GameManager.instance.perfectSize / 2),
                                    GameManager.instance.judgmentLocation[i] + (GameManager.instance.perfectSize / 2));
                                barControl.coolObj[i] = Instantiate(barControl.coolObjPrefab, barControl.createState.transform);
                                barControl.perfectObj[i] = Instantiate(barControl.perfectObjPrefab, barControl.createState.transform);
                                barControl.coolObj[i].transform.position = new Vector3(GameManager.instance.judgmentLocation[i], barControl.timingBar.transform.position.y, 0f);
                                barControl.perfectObj[i].transform.position = new Vector3(GameManager.instance.judgmentLocation[i], barControl.timingBar.transform.position.y, 0f);
                                GameManager.instance.hitIndex[i] = false;
                            }
                        }
                        GameManager.instance.currentJudgment = GameManager.instance.judgment;
                    }
                }
                GameManager.instance.isPerfect = false;
                GameManager.instance.isCool = false;
            }
            GameManager.instance.isSpecial = false;
        }
    }

    public void Button2()
    {
        if (GameManager.instance.currentJudgment <= 0)
        {
            Debug.Log("납품완료");
            if(!GameManager.instance.isFeverTime)
            {
                int point = (int)Random.Range(1, 11);
                GameManager.instance.specialPoint += point;
            }            
            // 다음 대기열의 물품에 대한 정보를 불러와 셋팅
            if (GameManager.instance.isFeverTime) GameManager.instance.judgment = 1;
            else
            {
                if (GameManager.instance.specialPoint >= 100)
                {
                    GameManager.instance.judgment = 1;
                    GameManager.instance.specialNPC = true;
                    GameManager.instance.specialPoint = 0;
                }
                else GameManager.instance.judgment = Random.Range(1, 4);
                
                
            }
                

            //GameManager.instance.movespeed += 100f;
            GameManager.instance.coolRange = new Vector2[GameManager.instance.judgment];
            GameManager.instance.perpectRange = new Vector2[GameManager.instance.judgment];
            GameManager.instance.judgmentLocation = new float[GameManager.instance.judgment];
            GameManager.instance.hitIndex = new bool[3];
            
            GameManager.instance.CreateJudgmentRange();
        }
        else
        {
            Debug.Log("제작 미완료");
        }
    }
}
