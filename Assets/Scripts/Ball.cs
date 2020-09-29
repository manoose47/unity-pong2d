using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    float speed;
    float radius;

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


        // if ball is hitting the bottom of boundary, invert its direction
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
            playBounce();
        }

        // the reverse of the above
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
            playBounce();
        }

        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        {
            playScore();
            gameManager.Reset(this.gameObject);
            gameManager.Score(true);
        }

        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            playScore();
            gameManager.Reset(this.gameObject);
            gameManager.Score(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;

            if (isRight && direction.x > 0)
            {
                playPaddle();
                direction.x = -direction.x;
            }
            if (!isRight && direction.x < 0)
            {
                playPaddle();
                direction.x = -direction.x;
            }
        }
    }

    private void playBounce()
    {
        _audioSource.clip = bounceSounds[Random.Range(0, bounceSounds.Length)];
        _audioSource.Play();
    }

    private void playPaddle()
    {
        _audioSource.clip = paddleSounds[Random.Range(0, paddleSounds.Length)];
        _audioSource.Play();
    }

    private void playScore()
    {
        _audioSource.clip = scoreSound;
        _audioSource.Play();
    }
}
