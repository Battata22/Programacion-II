using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Transform playerj;
    public AudioSource _audioSource;
    public AudioClip agarrado, error, tirar, sosteniendo;
    [SerializeField] float _rayDistance, _radius;
    public bool isHolding = false, sosteniendoBool = false;
    public float esperaragarre;
    Player _playerScript;
    Obj_Interactuable _objScript;
    [SerializeField] LayerMask _detectableMask;
    GameObject _lastObj;
    
    
    

    private void Awake()
    {
        _playerScript = GetComponentInParent<Player>();
        _audioSource = GetComponentInParent<AudioSource>();
        
    }

    private void Update()
    {
        esperaragarre += Time.deltaTime;

        Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red);

        RaycastHit hit;
        
        if (Physics.SphereCast(transform.position, _radius, transform.forward, out hit, _rayDistance, _detectableMask))
        {
            // Layer Objeto in slot 7
            bool interactuable = hit.transform.gameObject.layer == 7;
            //if (interactuable)
                _lastObj = hit.transform.gameObject;
            
            if (_lastObj.TryGetComponent<Pickable>(out Pickable p) && p.particleGen !=null && !p.particleGen.isPlaying && !isHolding)
            {
                Debug.Log($"<color=green>Particulas prendidas en </color> <color=purple> {_lastObj.name} </color>");
                p.particleGen.Play();
                p.parTime = Time.time;
            }
            //Debug.Log(interactuable);

            if (interactuable && !GameManager.Instance.HandState.holding && !GameManager.Instance.HandState.pointing)
            {
                GameManager.Instance.HandState.pointing = true;
                GameManager.Instance.HandState.relax = false;
                GameManager.Instance.HandState.ChangeState();
            }
            
            if (Input.GetMouseButtonDown(0) && interactuable)
            {
                _objScript = hit.transform.gameObject.GetComponent<Obj_Interactuable>();
                if (_objScript.grande == true && _playerScript._nivel >= 3)
                {
                    hit.transform.gameObject.GetComponent<Obj_Interactuable>().Interact(_audioSource, agarrado, error);
                }
                if (_objScript.mediano == true && _playerScript._nivel >= 2)
                {
                    hit.transform.gameObject.GetComponent<Obj_Interactuable>().Interact(_audioSource, agarrado, error);
                }
                if (_objScript.mediano == false && _objScript.grande == false && _playerScript._nivel >= 1)
                {
                    hit.transform.gameObject.GetComponent<Obj_Interactuable>().Interact(_audioSource, agarrado, error);
                }

            }

            if (Input.GetKeyDown(KeyCode.E) && interactuable)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance, _detectableMask))
                {
                    if (hit.transform.gameObject.GetComponent<SFX>() != null)
                    {
                        hit.transform.gameObject.GetComponent<SFX>().PlayMusic(error);
                    }
                    if (hit.transform.gameObject.GetComponent<BotonInicio>() != null)
                    {
                        hit.transform.gameObject.GetComponent<BotonInicio>().Teleport(playerj);
                    }
                }
                if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance * 1.5f, _detectableMask))
                {
                    if (hit.transform.gameObject.GetComponent<Luces>() != null)
                    {
                        hit.transform.gameObject.GetComponent<Luces>().LightSwitch();
                    }
                }
            }
        }
        else
        {
            if (_lastObj && _lastObj.TryGetComponent<Pickable>(out Pickable p) && p.particleGen != null && p.particleGen.isPlaying)
            {
                p.particleGen.Stop();
                Debug.Log($"<color=red>Particulas apagadas en </color> <color=yellow> {_lastObj.name} </color>");
            }
            if (GameManager.Instance.HandState.pointing)
            {
                GameManager.Instance.HandState.pointing = false;
                GameManager.Instance.HandState.ChangeState();
            }
        }

        if (isHolding == true)
        {
            if(sosteniendoBool == false && esperaragarre >= 0.05f)
            {
                sosteniendoBool = true;
                _audioSource.clip = sosteniendo;
                _audioSource.loop = true;
                _audioSource.Play();
            }

        }
        
    }
}
