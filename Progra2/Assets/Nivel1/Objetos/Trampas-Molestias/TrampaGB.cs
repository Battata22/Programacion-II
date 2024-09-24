using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaGB : MonoBehaviour
{
    [SerializeField] Chocamiento chocamientoScript;

    void Start()
    {

    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            chocamientoScript.Choco(player.transform.position);
            player._traped = true;
            //dejarte quieto 1s o 0.5s
            //relentizarte durante 5s
            //sacarte 1 de vida quiza?
        }

    }
}
