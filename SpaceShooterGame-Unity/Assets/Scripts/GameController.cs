using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController ThisInstance = null;
    public static int Score;
    public static int initScore = 0;
    public string ScorePrefix = string.Empty;
    public int scoreValue;
   
    public TextMeshProUGUI ScoreText = null;
    public TextMeshProUGUI GameOverText = null;
    public TextMeshProUGUI CongratulationsText = null;

    //Added for Replayability
    public GameObject Player = null;
    public Button button = null;

    void Awake()
    {
        ThisInstance = this;
        ScoreText.SetText(ScorePrefix);
    }

    void Update()
    {
        if(ScoreText != null)
        {
            ScoreText.SetText(ScorePrefix + Score.ToString());
        }

        scoreValue = Score;

        if(Player == null)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        if (ThisInstance.GameOverText != null)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length != 0)
            {
                ThisInstance.CongratulationsText.gameObject.SetActive(true);
            }
            else
            {
                ThisInstance.GameOverText.gameObject.SetActive(true);
            }
            ThisInstance.button.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Score = 0;
        ScorePrefix = "Score: ";
        ScoreText.SetText(ScorePrefix + initScore.ToString());
    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
