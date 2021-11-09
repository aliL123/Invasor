using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject manager;
    void Start()
    {
        scoreText.SetText(" ");
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetComponent<ScoreScript>().score == -1) // Player died score
        {
            scoreText.SetText("L Score");
        }
        else
        {
            scoreText.SetText("Final Score : " + manager.GetComponent<ScoreScript>().score);
        }


    }
}
