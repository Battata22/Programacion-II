using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonoros : MonoBehaviour
{
    [SerializeField] bool isPlaying = false, clip = false;
    AudioSource _audioSource;
    [SerializeField] AudioClip _cancion;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        //si el player le da a la e que se reproduzca
        //si le vuelve a dar que se pare (en pausa asi no se reinicia la cancion desde 0)
    }

    public virtual void PlayMusic()
    {
        if (isPlaying == false)
        {
            isPlaying = true;
            if (clip == false)
            {
                clip = true;
                _audioSource.clip = _cancion;
            }
            _audioSource.Play();
        }
        else if (isPlaying == true)
        {
            isPlaying = false;
            _audioSource.Pause();
        }
    }
}
