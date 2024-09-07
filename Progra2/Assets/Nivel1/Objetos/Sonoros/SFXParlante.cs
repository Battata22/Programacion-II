using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXParlante : SFX
{
    [SerializeField] AudioClip _cancionParlante1, _cancionParlante2, _cancionParlante3, _cancionParlante4, _cancionParlante5;

    public override void PlayMusic(AudioClip _clip1)
    {
        random1 = Random.Range(1, 5 + 1);

        if(random1 == 1)
        {
            base.PlayMusic(_cancionParlante1);
        }
        else if(random1 == 2)
        {
            base.PlayMusic(_cancionParlante2);
        }
        else if (random1 == 3)
        {
            base.PlayMusic(_cancionParlante3);
        }
        else if (random1 == 4)
        {
            base.PlayMusic(_cancionParlante4);
        }
        else if (random1 == 5)
        {
            base.PlayMusic(_cancionParlante5);
        }
        else
        {
            base.PlayMusic(_cancionParlante1);
        }

    }
}
