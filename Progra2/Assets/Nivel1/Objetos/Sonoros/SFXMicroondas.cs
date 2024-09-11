using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMicroondas : SFX
{
    [SerializeField] AudioClip _microondas;

    public override void PlayMusic(AudioClip _clip1)
    {
        base.PlayMusic(_microondas);
    }
}
