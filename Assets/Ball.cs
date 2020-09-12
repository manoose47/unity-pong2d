using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    float speed;
    float radius;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        // okay... hurray. This will associate our gameManager object by tag with the Ball. 
        // This allows us access to its class properties and functions with gameManager 
        // Whilst references with GameManager will access its static properties. Fucking hell
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
        }

        // the reverse of the above
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        {
            gameManager.Reset(this.gameObject);
            gameManager.Score(true);
        }

        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
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
                direction.x = -direction.x;
            }
            if (!isRight && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }
}
