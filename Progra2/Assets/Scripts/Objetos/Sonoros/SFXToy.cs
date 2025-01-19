using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXToy : SFX
{
    [SerializeField] AudioClip _toySound;
    public event DelegateType.VoidDelegateTrans OnSoundPlay = delegate { };

    protected override void Awake()
    {
        base.Awake();
        _audioClip = _toySound;
    }

    public override void PlayMusic(AudioClip _clip1)
    {
        base.PlayMusic(_toySound);
        _audioSource.clip = _toySound;

        OnSoundPlay(this.transform);
    }
}
