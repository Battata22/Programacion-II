using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class RaycastParaPlayeryNoModificarElOG : MonoBehaviour
{
    [SerializeField] float _rayDistance, _radius;
    [SerializeField] LayerMask _detectableMask;
    [SerializeField] Image E;
    [SerializeField] bool mirandoE = false;

    void Start()
    {
        mirandoE = false;
    }


    void Update()
    {
        if (mirandoE == true)
        {
            E.enabled = true;
        }
        else
        {
            E.enabled = false;
        }

        RaycastHit hit2;

        if (Physics.SphereCast(transform.position, _radius, transform.forward, out hit2, _rayDistance, _detectableMask))
        {

            if (hit2.collider.gameObject.TryGetComponent<Torreta>(out Torreta torretaScript))
            {
                mirandoE = true;
            }
            else if (hit2.collider.gameObject.TryGetComponent<BotonLaser>(out BotonLaser botonLaserScript))
            {
                mirandoE = true;
            }
            else
            {
                mirandoE = false;
            }

        }
        else
        {
            mirandoE = false;
        }


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
