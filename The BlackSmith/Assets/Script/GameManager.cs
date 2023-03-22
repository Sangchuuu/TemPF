using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
  
    public float barLocation;
    public Vector2[] coolRange;
    public Vector2[] perpectRange;
    public float[] judgmentLocation;
    [HideInInspector] public Vector2 judgmentRange;    
    public int currentJudgment;
    public int judgment = 3;
    [HideInInspector] public bool createJudge;
    public int count = 0;
    public float coolSize;
    public float perpectSize;
    public bool[] hitIndex;
    public bool isPerfect;
    public bool isCool;
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

    }
    void Start()
    {
        hitIndex = new bool[3];
        coolRange = new Vector2[judgment];
        perpectRange = new Vector2[judgment];
        judgmentLocation = new float[judgment];
        judge2Set = new Vector2[2];
        judge3Set = new Vector2[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateJudgmentRange()
    {
        
        float totalRange = -(judgmentRange.x) + judgmentRange.y;
        judge2Set[0] = new Vector2(judgmentRange.x, -(coolSize / 2));
        judge2Set[1] = new Vector2((coolSize / 2), judgmentRange.y);
        judge3Set[0] = new Vector2(judgmentRange.x, judgmentRange.x + (totalRange / 3) - (coolSize / 2));
        judge3Set[1] = new Vector2(judgmentRange.x + (totalRange / 3), judgmentRange.y - (totalRange / 3));
        judge3Set[2] = new Vector2(judgmentRange.y - (totalRange / 3) + (coolSize / 2), judgmentRange.y);

        for (int i = 0; i< judgment; i++)
        {
            if (judgment == 1)
            {
                float randomRange = Random.Range(judgmentRange.x, judgmentRange.y);
                coolRange[i] = new Vector2(randomRange - (coolSize / 2), randomRange + (coolSize / 2));
                perpectRange[i] = new Vector2(randomRange - (perpectSize / 2), randomRange + (perpectSize / 2));
                judgmentLocation[i] = randomRange;
            }
            if(judgment == 2)
            {
                float randomRange = Random.Range(judge2Set[i].x, judge2Set[i].y);
                coolRange[i] = new Vector2(randomRange - (coolSize / 2), randomRange + (coolSize / 2));
                perpectRange[i] = new Vector2(randomRange - (perpectSize / 2), randomRange + (perpectSize / 2));
                judgmentLocation[i] = randomRange;
            }
            if(judgment == 3)
            {
                float randomRange = Random.Range(judge3Set[i].x, judge3Set[i].y);
                coolRange[i] = new Vector2(randomRange - (coolSize / 2), randomRange + (coolSize / 2));
                perpectRange[i] = new Vector2(randomRange - (perpectSize / 2), randomRange + (perpectSize / 2));
                judgmentLocation[i] = randomRange;
            }
        }
        
        currentJudgment = judgment;
        count = 0;
        createJudge = true;                
    }
}
