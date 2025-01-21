using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Susto : AreasSustoYDuda
{
    public float scareAmount;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Nunca voy a aparecer porque ya no me usan");
    }

    private void OnTriggerEnter(Collider other)
    {
        //NPC _npcScript = other.GetComponent<NPC>();

        if (other.TryGetComponent<NPC>(out NPC _npcScript))
        {
            _npcScript.GetScared(scareAmount, actualRoom);
            asustado = true;
        }
    }
}
