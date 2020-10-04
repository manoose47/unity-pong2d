using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    public static Vector2 resetPoint;
    public static int leftScore;
    public static int rightScore;
    public Text scoreText;
    public Text stateText;

    int currentRally;
    int bestRally;

    int ricochetCount;

    int bestRicochetCount;

    private GameObject gameStateObject;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ball);
        ball.transform.position = resetPoint;

        // convert cameras screen coordinate into games screen coordinate
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Create 2 paddles, one on left and one on right, not sure why we have to cast
        Paddle paddle1 = Instantiate(paddle) as Paddle;
        Paddle paddle2 = Instantiate(paddle) as Paddle;
        paddle1.Init(true);
        paddle2.Init(false);
        leftScore = 0;
        rightScore = 0;

        // hide the game over or paused text at runtime
        gameStateObject = GameObject.FindGameObjectWithTag("ShowGameState");
        gameStateObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Score(bool isRightPaddle)
    {
        if (isRightPaddle)
        {
            rightScore++;
        }
        else
        {
            leftScore++;
        }

        UpdateScore(leftScore, rightScore);
    }

    private string UpdateScore(int leftScore, int rightScore)
    {
        scoreText.text = string.Format("{0} : {1}", leftScore, rightScore);
        if (leftScore > rightScore)
        {
            return ("Left player wins: " + leftScore + ":" + rightScore);
        }
        else if (rightScore > leftScore)
        {
            return ("Right player wins: " + rightScore + ":" + leftScore);
        }
        else
        {
            return ("The score is a draw: " + rightScore + ":" + leftScore);
        }
    }
    public void Reset(GameObject gameObject)
    {
        gameObject.transform.position = resetPoint;
    }

    public void GameOver()
    {
        scoreText.text = UpdateScore(leftScore, rightScore);
        stateText.text = "Game Over";
        gameStateObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void TogglePause()
    {
        stateText.text = "Paused";
        if (Time.timeScale == 1)
        {
            gameStateObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameStateObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void increasePaddleSpeed(float speed)
    {
        var paddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach (var paddle in paddles)
        {
            var paddleScript = paddle.GetComponent<Paddle>();
            if (paddleScript.speed < speed)
            {
                paddleScript.speed = speed;
            }
        }
    }

    public void updateRallyCounter(int rallyCount)
    {
        bestRally = rallyCount > bestRally ? rallyCount : bestRally;
        currentRally = rallyCount;
        Debug.Log("Rally count: " + rallyCount);
        Debug.Log("Best rally: " + bestRally);
    }
}
