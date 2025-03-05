using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RaycastParaPlayeryNoModificarElOG : MonoBehaviour
{
    [SerializeField] float _rayDistance, _radius;
    [SerializeField] LayerMask _detectableMask;

    void Start()
    {
    }


    void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (Physics.SphereCast(transform.position, _radius, transform.forward, out hit, _rayDistance, _detectableMask))
            {

                if(hit.collider.gameObject.TryGetComponent<Torreta>(out Torreta torretaScript))
                {
                    torretaScript.activado = true;
                }

                if (hit.collider.gameObject.TryGetComponent<BotonLaser>(out BotonLaser botonLaserScript))
                {
                    botonLaserScript.PressBoton();
                }

            }
        }

    }
}
