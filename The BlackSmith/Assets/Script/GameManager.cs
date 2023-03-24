using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("판정바 위치")]
    public float barLocation;

    [Header("판정바 스피드")]
    public float movespeed = 3f;

    [HideInInspector] public Vector2[] coolRange;
    [HideInInspector] public Vector2[] perpectRange;
    [HideInInspector] public Vector2 specialRange;
    [HideInInspector] public float[] judgmentLocation;
    [HideInInspector] public Vector2 judgmentRange;

    [Header("판정라인갯수 1~3")]
    public int currentJudgment;
    public int judgment = 3;

    [Header("특수손님")]
    public bool specialNPC;
    public int specialPoint = 0;
    
    [HideInInspector] public bool createJudge;
    //public int count = 0;
    
    [Header("판정라인 사이즈")]
    public float coolSize;
    public float perfectSize;
    public float specialSize;

    [Header("맞춘순서 기억")]
    public bool[] hitIndex;

    [HideInInspector] public bool isCool;
    [HideInInspector] public bool isPerfect;
    [HideInInspector] public bool isSpecial;
    
    [Header("피버")]
    public bool isFeverTime;
    public float feverTime = 10f;
    [HideInInspector] public bool feverIsOver;

    [HideInInspector] public Vector2[] judge2Set;
    [HideInInspector] public Vector2[] judge3Set;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        ParameterInit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FeverCheck();
    }

    public void FeverCheck()
    {
        if (isFeverTime)
        {
            feverTime -= Time.deltaTime;
        }

        if (feverTime < 0)
        {
            isFeverTime = false;            
            feverTime = 10f;
            feverIsOver = true;
        }
    }

    public void CreateJudgmentRange()
    {   
        if(specialNPC || isFeverTime)
        {
            float randomRange = Random.Range(judgmentRange.x, judgmentRange.y);
            specialRange = new Vector2(randomRange - (specialSize / 2), randomRange + (specialSize / 2));
            judgmentLocation[0] = randomRange;
        }
        else
        {
            float totalRange = -(judgmentRange.x) + judgmentRange.y;

            judge2Set[0] = new Vector2(judgmentRange.x, -(coolSize / 2));
            judge2Set[1] = new Vector2((coolSize / 2), judgmentRange.y);
            judge3Set[0] = new Vector2(judgmentRange.x, judgmentRange.x + (totalRange / 3) - (coolSize / 2));
            judge3Set[1] = new Vector2(judgmentRange.x + (totalRange / 3), judgmentRange.y - (totalRange / 3));
            judge3Set[2] = new Vector2(judgmentRange.y - (totalRange / 3) + (coolSize / 2), judgmentRange.y);

            for (int i = 0; i < judgment; i++)
            {
                if (judgment == 1)
                {
                    float randomRange = Random.Range(judgmentRange.x, judgmentRange.y);
                    coolRange[i] = new Vector2(randomRange - (coolSize / 2), randomRange + (coolSize / 2));
                    perpectRange[i] = new Vector2(randomRange - (perfectSize / 2), randomRange + (perfectSize / 2));
                    judgmentLocation[i] = randomRange;
                }
                if (judgment == 2)
                {
                    float randomRange = Random.Range(judge2Set[i].x, judge2Set[i].y);
                    coolRange[i] = new Vector2(randomRange - (coolSize / 2), randomRange + (coolSize / 2));
                    perpectRange[i] = new Vector2(randomRange - (perfectSize / 2), randomRange + (perfectSize / 2));
                    judgmentLocation[i] = randomRange;
                }
                if (judgment == 3)
                {
                    float randomRange = Random.Range(judge3Set[i].x, judge3Set[i].y);
                    coolRange[i] = new Vector2(randomRange - (coolSize / 2), randomRange + (coolSize / 2));
                    perpectRange[i] = new Vector2(randomRange - (perfectSize / 2), randomRange + (perfectSize / 2));
                    judgmentLocation[i] = randomRange;
                }
            }
        }
        currentJudgment = judgment;
        createJudge = true;
    }

    private void ParameterInit()
    {
        hitIndex = new bool[3];
        judge2Set = new Vector2[2];
        judge3Set = new Vector2[3];
        coolRange = new Vector2[judgment];
        perpectRange = new Vector2[judgment];
        judgmentLocation = new float[judgment];
    }
}
