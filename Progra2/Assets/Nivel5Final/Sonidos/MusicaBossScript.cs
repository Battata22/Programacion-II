using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaBossScript : MonoBehaviour
{
    [SerializeField] AudioSource source;
    public static bool prender = false, miPrimeritaVez = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
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
    }
}
