using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.SetText(" ");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText("Final Score : " + manager.GetComponent<ScoreScript>().score);
         
    }
}
