using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audio;
    public static SoundManager instance;

    public AudioClip damaged;
    public AudioClip explosion;
    public AudioClip countdown;
    public AudioClip acquire;
    public AudioClip gameOver;
    public AudioClip stageClear;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayDamaged()
    {
        audio.PlayOneShot(damaged);
    }

    public void PlayCountdown()
    {
        audio.PlayOneShot(countdown);
    }

    public void PlayExplosion()
    {
        audio.PlayOneShot(explosion);
    }

    public void PlayAcquire()
    {
        audio.PlayOneShot(acquire);
    }

    public void PlayGameOver() 
    {
        audio.PlayOneShot(gameOver);
    }
    public void PlayStageClear()
    {
        audio.PlayOneShot(stageClear);
    }

}
