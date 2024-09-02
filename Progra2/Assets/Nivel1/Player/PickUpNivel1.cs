using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpNivel1 : MonoBehaviour
{
    [SerializeField] float _rayDistance;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red);

        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance, LayerMask.GetMask("Objeto")))
            {
                hit.transform.gameObject.GetComponent<Obj_InteractuableNivel1>().Interact();
            }
        }
        
    }
}
