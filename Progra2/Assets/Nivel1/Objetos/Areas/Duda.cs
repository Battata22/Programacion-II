using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duda : AreasSustoYDuda
{
    protected override void Awake()
    {

    }

    protected override void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //NPC _npcScript = other.GetComponent<NPC>();

        if (other.TryGetComponent<NPC>(out NPC _npcScript))
        {
            _npcScript.GetDoubt(transform.position);
            
        }
    }
}
