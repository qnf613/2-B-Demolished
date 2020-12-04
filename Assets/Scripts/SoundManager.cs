using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audio;
    private AudioSource bgmAudio;
    public static SoundManager instance;

    public AudioClip damaged;
    public AudioClip explosion;
    public AudioClip countdown;
    public AudioClip acquire;
    public AudioClip gameOver;
    public AudioClip stageClear;
    public AudioClip electric;

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
        bgmAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
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
        bgmAudio.clip = null;
        audio.PlayOneShot(gameOver);
    }
    public void PlayStageClear()
    {
        bgmAudio.clip = null; 
        audio.PlayOneShot(stageClear);
    }

    public void PlayElectric()
    {
        audio.PlayOneShot(electric);
    }

}
