using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : Pickable
{
    public AudioSource _audioSource;
    [SerializeField] protected bool isPlaying = false, clip = false;
    protected int random1;

    public void Awake()
    {
        //_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public virtual void PlayMusic(AudioClip _audio1)
    {
        if (isPlaying == false)
        {
            isPlaying = true;
            if (clip == false)
            {
                clip = true;
                _audioSource.clip = _audio1;
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
