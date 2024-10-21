using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escritura : MonoBehaviour
{
    RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Physics.Raycast(transform.position, transform.forward, out hit);
            if (hit.collider != null && hit.transform.gameObject.GetComponent<Espejo>() != null)
            {
                hit.transform.gameObject.GetComponent<Espejo>().Escritura();
            }
        }

    }
}
