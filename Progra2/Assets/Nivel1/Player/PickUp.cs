using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip agarrado, error, tirar;
    [SerializeField] float _rayDistance;
    public bool isHolding = false;
    Player _playerScript;
    Obj_Interactuable _objScript;

    private void Awake()
    {
        _playerScript = GetComponentInParent<Player>();
        _audioSource = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red);

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {

           if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance, LayerMask.GetMask("Objeto")))
           {
               _objScript = hit.transform.gameObject.GetComponent<Obj_Interactuable>();
               if (_objScript.grande == true && _playerScript._nivel >= 3)
               {
                   hit.transform.gameObject.GetComponent<Obj_Interactuable>().Interact(_audioSource, agarrado, error);
               }
               else if (_objScript.mediano == true && _playerScript._nivel >= 2)
               {
                   hit.transform.gameObject.GetComponent<Obj_Interactuable>().Interact(_audioSource, agarrado, error);
               }
               else if (_objScript.mediano == false && _objScript.grande == false && _playerScript._nivel >= 1)
                {
                   hit.transform.gameObject.GetComponent<Obj_Interactuable>().Interact(_audioSource, agarrado, error);
               }
                    
           }

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance, LayerMask.GetMask("Objeto")))
            {
                if(hit.transform.gameObject.GetComponent<SFX>() != null)
                {
                    hit.transform.gameObject.GetComponent<SFX>().PlayMusic(error);
                }
            }
            if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance, LayerMask.GetMask("Objeto")))
            {
                if (hit.transform.gameObject.GetComponent<Luces>() != null)
                {
                    hit.transform.gameObject.GetComponent<Luces>().LightSwitch();
                }
            }
        }
        
    }
}
