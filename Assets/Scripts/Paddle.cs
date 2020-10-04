using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float speed;
    float height;
    string input;

    public bool isRight;

    void Start()
    {
        height = transform.localScale.y;

    }

    public void Init(bool isRightPaddle)
    {
        // Sets if paddle is right or left hand side on the paddle object, so that it can be referenced later
        isRight = isRightPaddle;

        Vector2 position = Vector2.zero;

        // defines Paddle starting position where x = maxRight || x = maxLeft, && y = 0
        if (isRightPaddle)
        {
            position = new Vector2(GameManager.topRight.x, 0);
            // offset the x position by the width of the paddle, so that its not clipping the edge
            position -= Vector2.right * transform.localScale.x;
            // assign the control scheme
            input = "PaddleRight";
        }
        else
        {
            position = new Vector2(GameManager.bottomLeft.x, 0);
            position -= Vector2.left * transform.localScale.x;
            input = "PaddleLeft";
        }

        transform.position = position;
        transform.name = input;

    }

    // Update is called once per frame
    void Update()
    {
        // input is 1 or 0, time.deltaTime will ensure that framerate doesnt effect movement speed, speed it custom var
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // prevent paddle moving off screen
        // if y is less than lowest point onscreen (+ height/2 for offset) && movement is down
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0)
        {
            move = 0;
        }
        // if y is greater than highest point onscreen (- height/2 for offset) && movement is up
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }

        transform.Translate(move * Vector2.up);
    }
}
