using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarBoton : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] LayerMask mask;



    void Start()
    {
     
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (Physics.SphereCast(transform.position, 0.15f, transform.forward, out hit, 100, mask))
            {
                Botones script = hit.collider.gameObject.GetComponent<Botones>();
                script.Trap();
            }
        }
    }
}
