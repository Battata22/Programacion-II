using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Susto : AreasSustoYDuda
{
    public float scareAmount;

    private void OnTriggerEnter(Collider other)
    {
        //NPC _npcScript = other.GetComponent<NPC>();

        if (other.TryGetComponent<NPC>(out NPC _npcScript))
        {
            _npcScript.GetScared(scareAmount);
            asustado = true;
        }
    }
}
