using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    public static Vector2 resetPoint;

    public static int leftScore;
    public static int rightScore;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void Score(bool isRightPaddle)
    {
        if (isRightPaddle)
        {
            rightScore++;
        }
        else
        {
            leftScore++;
        }

        if (leftScore > rightScore)
        {
            Debug.Log("Left player is winning: " + leftScore + ":" + rightScore);
        }
        else if (rightScore > leftScore)
        {
            Debug.Log("Right player is winning: " + rightScore + ":" + leftScore);
        }
        else
        {
            Debug.Log("The score is a draw: " + rightScore + ":" + leftScore);
        }

    }

    public static void Reset(GameObject gameObject)
    {
        gameObject.transform.position = resetPoint;
    }
}
