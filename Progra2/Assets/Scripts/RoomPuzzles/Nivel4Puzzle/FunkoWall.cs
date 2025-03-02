using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaFiesta;


namespace CasaFiesta
{
    public class FunkoWall : ObjetoColeccion
    {
        [SerializeField] Pickable[] _unChingoDeFunkos;

        protected override void BreakObj()
        {
            base.BreakObj();

            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

            foreach(var funco in _unChingoDeFunkos)
            {
                if(funco.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.constraints = RigidbodyConstraints.None;
                    rb.useGravity = true;
                }
            }

        }

    }

}