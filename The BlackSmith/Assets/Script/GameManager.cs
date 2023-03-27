using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("������ ��ġ")]
    public float barLocation;

    [Header("������ ���ǵ�")]
    public float movespeed;

    [HideInInspector] public Vector2[] coolRange;
    [HideInInspector] public Vector2[] perpectRange;
    [HideInInspector] public Vector2 specialRange;
    [HideInInspector] public float[] judgmentLocation;
    [HideInInspector] public Vector2 judgmentRange;

    [Header("�������ΰ��� 1~3")]
    public int currentJudgment;
    public int judgment;

    [Header("Ư���մ�")]
    public bool specialNPC;
    public int specialPoint;
    
    [HideInInspector] public bool createJudge;
    //public int count = 0;
    
    [Header("�������� ������")]
    public float coolSize;
    public float perfectSize;
    public float specialSize;

    [Header("������� ���")]
    public bool[] hitIndex;

    [HideInInspector] public bool isCool;
    [HideInInspector] public bool isPerfect;
    [HideInInspector] public bool isSpecial;
    [HideInInspector] public bool isMiss;

    [Header("�ǹ�")]
    public bool isFeverTime;
    public float feverTime;
    [HideInInspector] public bool feverIsOver;

    [HideInInspector] public Vector2[] judge2Set;
    [HideInInspector] public Vector2[] judge3Set;

    [Header("�ؽ�Ʈ ����")]
    public GameObject judgmentTextObj;
    public GameObject feverTextObj;
    public float judgmentTextTime;
    public float feverTextTime;
    public bool isJudgment;
    [HideInInspector] public TextMeshProUGUI judgmentText;
    [HideInInspector] public TextMeshProUGUI feverText;

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
        TextInit();
    }

    // Update is called once per frame
    void Update()
    {
        FeverCheck();
        OnOffText();
    }
    private void ParameterInit() // �ʱ�ȭ �Լ�
    {
        movespeed = 2f;
        judgment = 3;
        specialPoint = 0;
        feverTime = 10f;
        hitIndex = new bool[3];
        judge2Set = new Vector2[2];
        judge3Set = new Vector2[3];
        coolRange = new Vector2[judgment];
        perpectRange = new Vector2[judgment];
        judgmentLocation = new float[judgment];
        feverTextTime = feverTime;
    }

    public void FeverCheck() // �ǹ� ���� üũ
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

    public void CreateJudgmentRange() // ���������� ������ ��ġ ��� �� ����
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

    private void TextInit()
    {
        judgmentText = judgmentTextObj.GetComponent<TextMeshProUGUI>();
        judgmentText.text = " ";
        judgmentTextObj.SetActive(false);

        feverText = feverTextObj.GetComponent<TextMeshProUGUI>();
        feverText.text = "Fever Time !";
        feverTextObj.SetActive(false);
    }

    public void TextChange() // ���� �ؽ�Ʈ ����
    {
        // ������ �´� �ؽ�Ʈ ����
        if (isPerfect || isSpecial)
        {
            judgmentText.text = "Perfect !";
        }
        if (isCool)
        {
            judgmentText.text = "Cool !";
        }
        if (isMiss)
        {
            judgmentText.text = "Miss !";
        }
    }
    
    public void OnOffText()
    {
        if (isFeverTime) feverTextObj.SetActive(true);
        if (feverIsOver) feverTextObj.SetActive(false);

        judgmentTextTime -= Time.deltaTime;

        if (isJudgment)
        {
            judgmentTextTime = 1f;
            judgmentTextObj.SetActive(true);
            isJudgment = false;
        }

        if (judgmentTextTime < 0)
        {
            judgmentTextTime = 0;
            judgmentTextObj.SetActive(false);
        }
    }
}
