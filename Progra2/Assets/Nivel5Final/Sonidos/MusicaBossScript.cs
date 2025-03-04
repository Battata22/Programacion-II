using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaBossScript : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip music, victory;
    public static bool prender = false, miPrimeritaVez = false, cancionfinal = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = music;
        prender = false;
        miPrimeritaVez = false;
        cancionfinal = false;
    }


    void Update()
    {
        if (prender == true && source.isPlaying == false && miPrimeritaVez == false)
        {
            source.Play();
            miPrimeritaVez = true;
        }

        if (Time.timeScale == 0 && source.isPlaying == true)
        {
            source.Pause();
        }
        else if (Time.timeScale != 0 && source.isPlaying == false && miPrimeritaVez == true)
        {
            source.Play();
        }

        if (cancionfinal == true)
        {
            CancionVictory();
        }

    }

    void CancionVictory()
    {
        if (source.clip != victory)
        {
            source.clip = victory;
            source.Play();
            cancionfinal = false;
        }
    }
}
