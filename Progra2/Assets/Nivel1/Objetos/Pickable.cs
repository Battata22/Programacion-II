using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Chocamiento))]
public class Pickable : Obj_Interactuable
{
    bool _pickedUp, _trowed;
    public PickUp pickUpScript;
    
    //Material _originalMaterial;

    private void Start()
    {      
        //_camera = GameManager.Instance.Camera.transform;
        //_itemHolder = GameManager.Instance.ItemHolde;
        //pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        //_materialNormal = GetComponent<Material>();
        
        _speed = 10f;
        _cd = 1;
        if (mediano)
        {
            _cd = 2;
            _rb.mass = 4f;          
        }
        if (grande)
        {
            _cd = 3;
            _rb.mass = 7f;
        }
        if ((mediano || grande) && !GetComponent<NavMeshObstacle>())
        { 
            this.AddComponent<NavMeshObstacle>();
        }
        _dropLayers = GameManager.Instance.DropLayers;
    }

    private void Update()
    {
        //if(_materialNormal == null) _materialNormal = GetComponent<Material>();
        if (_camera == null) _camera = GameManager.Instance.Camera.transform;
        if(_itemHolder == null) _itemHolder = GameManager.Instance.ItemHolde;
        if(pickUpScript == null) pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (holding && Input.GetMouseButtonDown(1))
        {
            Throw(pickUpScript._audioSource, pickUpScript.tirar);
            pickUpScript.sosteniendoBool = false;
            pickUpScript._audioSource.loop = false;
        }

        if (holding && Input.GetMouseButtonDown(0))
        {
            Drop();
            pickUpScript.sosteniendoBool = false;
            pickUpScript._audioSource.loop = false;
        }

        if(holding && !_trowed)
        {
            CheckForDrop();
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Movement(_itemHolder);
        }
    }
    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {

        if (pickUpScript.isHolding == false && Time.time - _lastInteract > _cd)
        {
            _lastInteract = Time.time;
            base.Interact(_audio, agarre, error);
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
            _canMove = true;
            _pickedUp = true;
            pickUpScript.isHolding = true;
            _onAir = true;
            _col.enabled = false;
            _rb.constraints = RigidbodyConstraints.None;
            pickUpScript.esperaragarre = 0;
            _renderer = GetComponent<Renderer>();
            _renderer.material = _materialFade;
        }


    }

    public override void Throw(AudioSource _audio, AudioClip arrojar)
    { 
        base.Throw(pickUpScript._audioSource, pickUpScript.tirar);
        _trowed = true;
        _col.enabled = true;
        _pickedUp = false;
        pickUpScript.isHolding = false;
        _onAir = true;
        _renderer.material = _materialNormal;
        _audio.clip = arrojar;
        _audio.Play();

    }

    public void Drop()
    {
        //if (holding == false) return;
        _lastInteract= Time.time;

        GameManager.Instance.HandState.holding = false;
        GameManager.Instance.HandState.pointing = false;
        GameManager.Instance.HandState.relax = true;
        GameManager.Instance.HandState.ChangeState();

        _col.enabled = true;
        _pickedUp = false;
        pickUpScript.isHolding = false;
        _onAir = false;
        holding = false;
        _canMove = false;
        _rb.useGravity = true;
        _renderer.material = _materialNormal;
        GameManager.Instance.Player.GetComponent<AudioSource>().Stop();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //if (canMove) Drop();
        if(_onAir)
        {
            //Collider[] hitObjs = Physics.OverlapSphere(transform.position, 0.5f, _layerMask);
            if (_trowed && TryGetComponent<Chocamiento>(out Chocamiento choc))
            {
                //Chocamiento choc = GetComponent<Chocamiento>();
                //if (TryGetComponent<Chocamiento>(out Chocamiento choc))//choc != null && 
                //{
                //    choc.Choco(transform.position);
                //    onAir = false;
                //}
                choc.Choco(transform.position);
                _onAir = false;
                _trowed = false;
            }
            //else if (hitObjs.Length != 0)//collision.gameObject.layer != 7
            //{
            //    Drop(); 
            //}
        }
    }

    Collider[] hitObjs;
    void CheckForDrop()
    {
        Collider[] hitObjs = Physics.OverlapSphere(transform.position, 0.5f, _dropLayers);
        if (hitObjs.Length != 0)//collision.gameObject.layer != 7
        {
            Drop();
        }
    }

}
