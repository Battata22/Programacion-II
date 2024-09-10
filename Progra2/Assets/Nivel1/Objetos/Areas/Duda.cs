using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duda : AreasSustoYDuda
{
    private void OnTriggerEnter(Collider other)
    {
        NPC _npcScript = other.GetComponent<NPC>();

        if (_npcScript != null)
        { 
            _npcScript.GetDoubt(transform.position);
            //hacer que no se active si tambien se activa el susto
        }
    }
}
