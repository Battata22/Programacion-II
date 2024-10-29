using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessNPC : MonoBehaviour
{
    [SerializeField] float _radius, _rayDistance, carga, cargaTimer;
    [SerializeField] LayerMask _detectableMask;

    Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {

        //if (Input.GetKey(KeyCode.Q) && carga <= tiempoDeCarga + 0.3)
        //{
        //    carga += Time.deltaTime;
        //}
        //else 
        //{
        //    if (carga > 0)
        //    {
        //        carga -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        carga = 0;
        //    }
        //}



        Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.blue);

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, _radius, transform.forward, out hit, _rayDistance, _detectableMask))
        {
            var asustable = hit.transform.GetComponent<Asustable>();
            if (Input.GetKeyDown(KeyCode.Q) /*carga >= cargaTimer */  && hit.transform.TryGetComponent<IPossessable>(out IPossessable target) && asustable.stuned)
            {   
                target.GetPossess();
                _player.StartPossession();
                
            }
        }
    }
}
