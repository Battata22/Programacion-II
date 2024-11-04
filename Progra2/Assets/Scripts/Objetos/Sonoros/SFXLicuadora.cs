using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLicuadora : SFX
{
    [SerializeField] AudioClip _licuadora;
    protected override void Awake()
    {
        base.Awake();
        _audioClip = _licuadora;
    }

    public override void PlayMusic(AudioClip _clip1)
    {
        base.PlayMusic(_licuadora);
    }
}
