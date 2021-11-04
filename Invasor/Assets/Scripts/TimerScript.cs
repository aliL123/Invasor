using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 180;
    public bool timerIsRunning = false;
    public Text timeText;
    public GameObject panel;
    public bool allEnemiesDead;
    public List<GameObject> enemies;

    private void Start()
    {
        
        panel.SetActive(false);
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else if (timeRemaining <= 0 || allEnemiesDead)
            {
                StartCoroutine(ChangeLevel());
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = (string.Format("{0:00}:{1:00}", minutes, seconds));
    }
    IEnumerator ChangeLevel()
    {

        panel.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        Debug.Log("In Scene");
        timeRemaining = 0;
        timerIsRunning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
}