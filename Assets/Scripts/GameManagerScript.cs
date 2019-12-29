using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private Scene scene;
    private string nickname;
    private Text Nickname;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name.Equals("Ranking"))
        {
            string line;
            string[] values = new string[2];

            string[] nicknames = new string[5];
            string[] scores = new string[5];

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\szymo\unity-workspace\Project-Game-3D\Assets\Scripts\Ranking.txt");

            for (int i = 0; i < 5; i++)
            {
                line = file.ReadLine();
                values = line.Split(';');

                nicknames[i] = values[0];
                scores[i] = values[1];
            }

            file.Close();
            DisplayRanking(nicknames, scores);
        }

        else if (scene.name.Equals("GameOver"))
        {
            //TODO:
            //getting these values & writing into txt file
            DisplayResult("Szymcio", "12345", "OUT of 5");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        Nickname = GameObject.Find("Nickname").GetComponent<Text>();
        nickname = Nickname.text.ToString();

        if (!nickname.Equals("") && !nickname.Contains(";"))
        {
            //TODO:
            //to save nickname in memory
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }

    public void LoadRanking()
    {
        SceneManager.LoadScene("Ranking", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void DisplayRanking(string[] nicknames, string[] scores)
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject.Find("Nick" + (i + 1) + "").GetComponent<Text>().text = nicknames[i];
            GameObject.Find("Score" + (i + 1) + "").GetComponent<Text>().text = scores[i];
        }
    }

    public void DisplayResult(string nick, string score, string rank)
    {
        GameObject.Find("NickText").GetComponent<Text>().text = nick;
        GameObject.Find("ScoreText").GetComponent<Text>().text = score;
        GameObject.Find("RankText").GetComponent<Text>().text = rank;
    }
}