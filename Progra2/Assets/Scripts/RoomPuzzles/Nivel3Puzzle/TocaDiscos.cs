using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocaDiscos : SFX
{
    [Header("<color=blue>Tocadiscos</color>")]
    [SerializeField] AudioClip _noise;
    //[SerializeField] Disco currentDisc;
    AudioClip _musicDisc;
    bool useDisc;

    public event DelegateType.VoidDelegate OnDiscPlay = delegate { };

    protected override void Awake()
    {
        base.Awake();
        _audioClip = _noise;
    }

    public override void PlayMusic(AudioClip _audio1)
    {
        if(useDisc)
        {
            base.PlayMusic(_musicDisc);
            _audioSource.clip = _musicDisc;
            OnDiscPlay();
        }
        else
        {
            base.PlayMusic(_noise);
            _audioSource.clip = _noise;
        }
    }

    public void ChangeDisc(AudioClip newAudio)
    {
        useDisc = true;
        _musicDisc = newAudio;

        PlayMusic(newAudio);
    }
}
