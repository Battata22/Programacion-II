using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTV : SFX
{
    [SerializeField] AudioClip _tV;
    public override void PlayMusic(AudioClip _clip1)
    {
            base.PlayMusic(_tV);
    }
}
