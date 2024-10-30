using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessNPC : MonoBehaviour
{
    [SerializeField] float _radius, _rayDistance, carga, cargaTimer;
    [SerializeField] LayerMask _detectableMask;
    [SerializeField] Slider cargaVisual;

    Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        cargaVisual.maxValue = cargaTimer;
        cargaVisual.minValue = 0;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Q) && carga <= cargaTimer + 0.3)
        {
            cargaVisual.gameObject.SetActive(true);
            carga += Time.deltaTime;
        }
        else
        {
            if (carga > 0)
            {
                carga -= Time.deltaTime;
            }
            else
            {
                carga = 0;
                cargaVisual.gameObject.SetActive(false);
            }
        }


        cargaVisual.value = carga;


        //Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.blue);

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, _radius, transform.forward, out hit, _rayDistance, _detectableMask))
        {
            var asustable = hit.transform.GetComponent<Asustable>();
            if (/*Input.GetKeyDown(KeyCode.Q)*/ carga >= cargaTimer && hit.transform.TryGetComponent<IPossessable>(out IPossessable target) && asustable.stuned)
            {   
                target.GetPossess();
                _player.StartPossession();
                
            }
        }
    }
}
