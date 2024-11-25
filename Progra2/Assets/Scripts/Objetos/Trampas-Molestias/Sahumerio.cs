using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sahumerio : MonoBehaviour
{
    [SerializeField] float checkCooldown, radioCheck;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] bool prendido = false;


    void Start()
    {
        
    }


    void Update()
    {
        CheckRoom();
    }

    void CheckRoom()
    {
        if (prendido)
        {
            Collider[] collCercanos = Physics.OverlapSphere(transform.position, radioCheck, targetLayer);
            foreach (Collider coll in collCercanos)
            {
                coll.gameObject.TryGetComponent<CollidersCasa2>(out CollidersCasa2 collCasa2Script);
                if (collCasa2Script != null)
                {
                    collCasa2Script.sahumerioDentro = true;
                    //HAY QUE APAGARLO
                }
            }
        }

    }
}
