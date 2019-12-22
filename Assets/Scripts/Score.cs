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
    // Update is called once per frame
    void Update()
    {
        if(score>=scoreNextLevel)
        {
            LevelUp();
        }
        score += Time.deltaTime * difficultyLevel;
        scoreText.text =((int) score).ToString();
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
