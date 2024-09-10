using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Susto : AreasSustoYDuda
{
    private void OnTriggerEnter(Collider other)
    {
        NPC _npcScript = other.GetComponent<NPC>();

        if (_npcScript != null)
        {
            _npcScript.GetScare();
            asustado = true;
        }
    }
}
