using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private string nickname;
    private Text Nickname;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if (!nickname.Equals(""))
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
}
