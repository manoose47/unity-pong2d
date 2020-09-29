using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timeText;
    private GameManager gameManager;

    bool timerIsRunning = false;
    void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameManager");
        if (controller != null)
        {
            gameManager = controller.GetComponent<GameManager>();
        }
        else
        {
            Debug.Log("Cannot find game Manager");
        }
        timerIsRunning = true;
    }


    // The time time to display in seconds, editable in unity
    [SerializeField]
    public float timeRemaining = 10;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    gameManager.TogglePause();
                }
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                // lock time to 0, because update will keep running, and timer will likely be in negative numbers
                timeRemaining = 0;
                timerIsRunning = false;
                gameManager.GameOver();
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        // prevents weird shit such as 0:00 going into a negative number
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
