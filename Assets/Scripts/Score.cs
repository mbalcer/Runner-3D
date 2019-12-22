using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private float score = 2.85f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 20;
    private int scoreNextLevel = 10;
    public Text scoreText;
    private StarsManager starManager;
    // Update is called once per frame

    void Start()
    {
        starManager = GameObject.Find("StarManager").GetComponent<StarsManager>();
    }
    void Update()
    {
        if(score>=scoreNextLevel)
        {
            LevelUp();
        }
        score += Time.deltaTime * difficultyLevel;
        int allScore = (int)score + starManager.GetStar() * 2;
        scoreText.text =allScore.ToString();
    }
    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;
        scoreNextLevel *= 2;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
        Debug.Log(difficultyLevel);
    }
}
