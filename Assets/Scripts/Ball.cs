using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Secret messages

public class Ball : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    float speed;
    float radius;
    float startingSpeed;
    int paddleCounter;
    int hitCounter;

    public AudioClip[] bounceSounds;

    public AudioClip[] paddleSounds;

    public AudioClip scoreSound;

    public AudioSource _audioSource;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        // okay... hurray. This will associate our gameManager object by tag with the Ball. 
        // This allows us access to its class properties and functions with gameManager 
        // Whilst references with GameManager will access its static properties. Fucking hell
        _audioSource = GetComponent<AudioSource>();
        speed = speed > 0 ? speed : 5.0f;
        startingSpeed = speed;
        paddleCounter = 0;
        hitCounter = 0;

        GameObject controller = GameObject.FindGameObjectWithTag("GameManager");
        if (controller != null)
        {
            gameManager = controller.GetComponent<GameManager>();
        }
        else
        {
            Debug.Log("Cannot find game Manager");
        }
        direction = Vector2.one.normalized; // starting direction
        radius = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (touchingVerticalBoundary())
        {
            ricochet(true);
            playAudio(bounceSounds, null);
        }

        if (transform.position.x < GameManager.bottomLeft.x && direction.x < 0)
        {
            playScore();
            resetSpeed();
            paddleCounter = 0;
            gameManager.updateRallyCounter(0);
            gameManager.Reset(this.gameObject);
            gameManager.Score(true);
        }

        if (transform.position.x > GameManager.topRight.x && direction.x > 0)
        {
            playScore();
            resetSpeed();
            paddleCounter = 0;
            gameManager.updateRallyCounter(0);
            gameManager.Reset(this.gameObject);
            gameManager.Score(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (touchingPaddle(other))
        {
            paddleCounter++;
            if (paddleCounter % 2 == 0)
            {
                gameManager.updateRallyCounter(paddleCounter / 2);
            }
            ricochet(false);
            playAudio(paddleSounds, null);
        }
    }
    private void playScore()
    {
        _audioSource.clip = scoreSound;
        _audioSource.Play();
    }

    private void increaseSpeed()
    {
        speed++;
        gameManager.increasePaddleSpeed(speed);
    }

    private void resetSpeed()
    {
        speed = startingSpeed;
    }

    private bool touchingVerticalBoundary()
    {
        var ballTouchingBottomBoundary = transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0;
        var ballTouchingTopBoundary = transform.position.y > GameManager.topRight.y - radius && direction.y > 0;

        return ballTouchingBottomBoundary || ballTouchingTopBoundary;
    }

    private void ricochet(bool isVerticalRicochet)
    {
        if (isVerticalRicochet)
        {
            direction.y = -direction.y;
        }
        else
        {
            direction.x = -direction.x;
        }

        hitCounter++;
        gameManager.UpdateRicochetCounter(hitCounter);
        increaseSpeed();
    }

    private void playAudio(AudioClip[] clips, AudioClip clip)
    {
        _audioSource.clip = clips != null ? clips[Random.Range(0, clips.Length)] : clip;
        _audioSource.Play();
    }

    private bool touchingPaddle(Collider2D other)
    {
        if (other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;

            var touchingRightPaddle = isRight && direction.x > 0;
            var touchingLeftPaddle = !isRight && direction.x < 0;

            return touchingRightPaddle || touchingLeftPaddle;
        }
        return false;
    }

}
