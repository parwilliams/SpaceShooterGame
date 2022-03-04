using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDestroy : MonoBehaviour
{
    public int ScoreValue = 50;

    void OnDestroy()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0) //Keep cloned enemies from scoring after game ends
        {
            GameController.Score += ScoreValue;
            Debug.Log("Score");
        }
        
    }
}
