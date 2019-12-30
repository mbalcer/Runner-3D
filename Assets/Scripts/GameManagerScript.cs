using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;

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
            string[] data = ReadData();
            DisplayRanking(data);
        }

        else if (scene.name.Equals("GameOver"))
        {
            string nickname = DataManager.GetNickname();
            int score = DataManager.GetScore();

            int rank = CheckData(nickname, score);
            string place = rank != 0 ? rank.ToString() : "OUT of TOP 5";

            DisplayResult(nickname, score.ToString(), place);
        }
    }

    private string[] ReadData()
    {
        string[] data = new string[10];

        System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\szymo\unity-workspace\Project-Game-3D\Assets\Scripts\Ranking.txt");

        for (int i = 0; i < 5; i++)
        {
            string line = file.ReadLine();
            string[] values = new string[2];

            try
            {
                values = line.Split(';');
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Error while string splitting: " + e);
            }

            data[i * 2] = values[0];
            data[(i * 2) + 1] = values[1];
        }

        file.Close();

        return data;
    }
    
    private void DisplayRanking(string[] data)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject.Find("Nick" + (i + 1) + "").GetComponent<Text>().text = data[i * 2];
            GameObject.Find("Score" + (i + 1) + "").GetComponent<Text>().text = data[i * 2 + 1];
        }
    }

    private int CheckData(string nick, int score)
    {
        string[] data = ReadData();
        
        for (int i = 0; i < 5; i++)
        {
            int.TryParse(data[(i * 2) + 1], out int value);

            if (score > value)
            {
                string nowNick = data[i * 2];
                string nowScore = data[(i * 2) + 1];
                string nextNick, nextScore;

                for (int j = i; j < 4; j++)
                {
                    nextNick = data[(j + 1) * 2];
                    nextScore = data[((j + 1) * 2) + 1];

                    data[(j + 1) * 2] = nowNick;
                    data[((j + 1) * 2) + 1] = nowScore;

                    nowNick = nextNick;
                    nowScore = nextScore;
                }

                data[i * 2] = nick;
                data[(i * 2) + 1] = score.ToString();

                WriteData(data);

                return i + 1;
            }
        }

        return 0;
    }

    private void WriteData(string[] data)
    {
        string text = "";

        for (int i = 0; i < 5; i++)
        {
            string nick = data[i * 2];
            string score = data[(i * 2) + 1];

            if (nick != null && score != null)
            {
                text += nick + ";" + score + Environment.NewLine;
            }
        }
        
        System.IO.File.WriteAllText(@"C:\Users\szymo\unity-workspace\Project-Game-3D\Assets\Scripts\Ranking.txt", text);
    }

    private void DisplayResult(string nick, string score, string rank)
    {
        GameObject.Find("NickText").GetComponent<Text>().text = nick;
        GameObject.Find("ScoreText").GetComponent<Text>().text = score;

        if(rank.Equals("OUT of TOP 5")) {
            GameObject.Find("RankText").GetComponent<Text>().fontSize = 20;
        }
        else {
            GameObject.Find("RankText").GetComponent<Text>().fontSize = 40;
        }

        GameObject.Find("RankText").GetComponent<Text>().text = rank;
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
            DataManager.SetNickname(nickname);
            ReloadGame();
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void LoadRanking()
    {
        SceneManager.LoadScene("Ranking", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}