using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public BarControl barControl;

    public void HitButton()
    {
        if (GameManager.instance.count != GameManager.instance.judgment)
        {
            if (GameManager.instance.currentJudgment > 0)
            {
                for (int index = 0; index < GameManager.instance.judgment; index++)
                {
                    if (GameManager.instance.barLocation < GameManager.instance.perpectRange[index].y && GameManager.instance.barLocation > GameManager.instance.perpectRange[index].x)
                    {
                        //Perpect �����϶�
                        Debug.Log("Perpect!");
                        GameManager.instance.isPerfect = true;
                        GameManager.instance.currentJudgment--;
                        GameManager.instance.coolRange[index] = Vector2.zero;
                        barControl.coolObj[index].SetActive(false);
                        GameManager.instance.perpectRange[index] = Vector2.zero;
                        barControl.perpectObj[index].SetActive(false);
                        Debug.Log("������Ʈ����");
                        GameManager.instance.hitIndex[index] = true;
                        break;
                    }
                }
                if(!GameManager.instance.isPerfect)
                {
                    for (int index = 0; index < GameManager.instance.judgment; index++)
                    {
                        if (GameManager.instance.barLocation < GameManager.instance.coolRange[index].y && GameManager.instance.barLocation > GameManager.instance.coolRange[index].x)
                        {

                            //Cool �����϶�
                            Debug.Log("Cool!");
                            GameManager.instance.isCool = true;
                            GameManager.instance.currentJudgment--;
                            GameManager.instance.coolRange[index] = Vector2.zero;
                            barControl.coolObj[index].SetActive(false);
                            GameManager.instance.perpectRange[index] = Vector2.zero;
                            barControl.perpectObj[index].SetActive(false);
                            Debug.Log("������Ʈ����");
                            GameManager.instance.hitIndex[index] = true;
                            break;
                        }
                    }
                }                
                if (!GameManager.instance.isPerfect && !GameManager.instance.isCool)
                {
                    //Miss �����϶�
                    Debug.Log("Miss!");
                    if (GameManager.instance.currentJudgment != GameManager.instance.judgment)
                    {
                        for (int i = 0; i < GameManager.instance.judgment; i++)
                        {
                            if (GameManager.instance.hitIndex[i])
                            {
                                GameManager.instance.coolRange[i] = new Vector2(GameManager.instance.judgmentLocation[i] - (GameManager.instance.coolSize / 2),
                                    GameManager.instance.judgmentLocation[i] + (GameManager.instance.coolSize / 2));
                                GameManager.instance.perpectRange[i] = new Vector2(GameManager.instance.judgmentLocation[i] - (GameManager.instance.perpectSize / 2),
                                    GameManager.instance.judgmentLocation[i] + (GameManager.instance.perpectSize / 2));
                                barControl.coolObj[i].SetActive(true);
                                barControl.perpectObj[i].SetActive(true);
                                Debug.Log("������Ʈ����");
                                GameManager.instance.hitIndex[i] = false;
                            }
                        }
                        GameManager.instance.currentJudgment = GameManager.instance.judgment;
                    }
                }
                GameManager.instance.isPerfect = false;
                GameManager.instance.isCool = false;
            }
        }
    }

    public void Button2()
    {
        if (GameManager.instance.currentJudgment <= 0)
        {
            Debug.Log("��ǰ�Ϸ�");

            // ���� ��⿭�� ��ǰ�� ���� ������ �ҷ��� ����
            // GameManager.instance.judgment = ��ǰ ���� ����;

            GameManager.instance.coolRange = new Vector2[GameManager.instance.judgment];
            GameManager.instance.perpectRange = new Vector2[GameManager.instance.judgment];
            GameManager.instance.judgmentLocation = new float[GameManager.instance.judgment];
            GameManager.instance.hitIndex = new bool[3];
            
            GameManager.instance.CreateJudgmentRange();
        }
        else
        {
            Debug.Log("���� �̿Ϸ�");
        }
    }
}
