using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController ThisInstance = null;
    public static int Score;
    public string ScorePrefix = string.Empty;
   
    public Text ScoreText = null;
    public Text GameOverText = null;

    //Added for Replayability
    public GameObject Player = null;
    public Button button = null;

    void Awake()
    {
        ThisInstance = this;
    }

    void Update()
    {
        if(ScoreText != null)
        {
            ScoreText.text = ScorePrefix + Score.ToString();
        }

        if(Player == null)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        if(ThisInstance.GameOverText != null)
        {
            ThisInstance.GameOverText.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Score = 0;
        ScoreText = null;
        ScorePrefix = string.Empty;
    }
}
