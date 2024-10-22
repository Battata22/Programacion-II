using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSusto : MonoBehaviour
{
    [SerializeField] Espejo espejoScript;
    [SerializeField] bool asuste = false;

    private void Start()
    {
        espejoScript = GetComponentInParent<Espejo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (espejoScript.activo == true && other.GetComponent<Asustable>() != null && asuste == false)
        {
            Asustable asus = other.GetComponent<Asustable>();
            asus.GetScared(0.5f);
            asuste = true;
        }
    }
}
