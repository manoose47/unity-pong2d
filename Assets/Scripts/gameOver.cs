using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject gameStateObject;

    [Header("Text Fields")]
    public Text scoreText;
    public Text stateText;
    public Text rallyText;
    public Text ricochetText;
    void Start()
    {
        var stats = State.GetStats();
        scoreText.text = stats.Score;
        stateText.text = stats.Winner;
        rallyText.text = "Best rally: " + stats.Rallies;
        ricochetText.text = "Score: " + stats.Ricochets;
        gameStateObject = GameObject.FindGameObjectWithTag("ShowGameState");
        gameStateObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
