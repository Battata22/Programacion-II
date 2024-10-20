using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTV : SFX
{
    [SerializeField] AudioClip _tV;
    [SerializeField] GameObject _pantalla;

    protected override void Awake()
    {
        base.Awake();
        _audioClip = _tV;
    }
    public override void PlayMusic(AudioClip _clip1)
    {
        if (isPlaying == false)
        {
            _pantalla.SetActive(true);
        }
        else if (isPlaying == true)
        {
            _pantalla.SetActive(false);
        }
        base.PlayMusic(_tV);   
    }
}
