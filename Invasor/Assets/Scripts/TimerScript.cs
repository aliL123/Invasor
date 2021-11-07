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

    private void Start()
    {
        panel.SetActive(false);
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
<<<<<<< HEAD
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
=======
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
<<<<<<< HEAD
            if (totalEnemyCount <= 0)
            {
                StartCoroutine(ChangeLevel());
            }
            else if (timeRemaining <= 0)
=======
            else
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
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
        Time.timeScale = 0; // freezes scene
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1; // unfreezes scene
        timeRemaining = 0;
        timerIsRunning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // level 2


    }
}