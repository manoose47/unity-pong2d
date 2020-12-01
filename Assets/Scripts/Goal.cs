using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public bool isGoalRHS = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Score(isGoalRHS);
        }
    }
}
