using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    // Start is called before the first frame update

    private int ricochetCounter;
    private AudioSource _audioSource;
    public AudioClip[] bounceSounds;

    void Start()
    {
        ricochetCounter = 0;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            ricochetCounter++;
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateRicochetCounter(ricochetCounter);
            playAudio(bounceSounds, null);
        }
    }

    private void playAudio(AudioClip[] clips, AudioClip clip)
    {
        _audioSource.clip = clips != null ? clips[Random.Range(0, clips.Length)] : clip;
        _audioSource.Play();
    }
}
