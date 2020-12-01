using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float speed;

    [SerializeField]
    public Rigidbody2D paddleRigidBody;

    [SerializeField]
    public AudioClip[] paddleSounds;

    private float movement;

    public bool isRight;

    private Vector2 startPosition;
    private int paddleCounter;

    private AudioSource _audioSource;

    void Start()
    {
        startPosition = transform.position;
        paddleCounter = 0;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isRight)
        {
            movement = Input.GetAxisRaw("PaddleRight");
        }
        else
        {
            movement = Input.GetAxisRaw("PaddleLeft");
        }

        paddleRigidBody.velocity = new Vector2(paddleRigidBody.velocity.x, movement * speed);
    }

    public void Reset()
    {
        transform.position = startPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            paddleCounter++;
            if (paddleCounter % 2 == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().updateRallyCounter(paddleCounter / 2);
            }
            playAudio(paddleSounds, null);
        }
    }

    private void playAudio(AudioClip[] clips, AudioClip clip)
    {
        _audioSource.clip = clips != null ? clips[Random.Range(0, clips.Length)] : clip;
        _audioSource.Play();
    }
}
