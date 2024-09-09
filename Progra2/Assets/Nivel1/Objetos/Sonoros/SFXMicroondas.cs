using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMicroondas : SFX
{
    [SerializeField] AudioClip _microondas;

    private void Awake()
    {
        mediano = true;
    }
    public override void PlayMusic(AudioClip _clip1)
    {
        base.PlayMusic(_microondas);
    }
}
