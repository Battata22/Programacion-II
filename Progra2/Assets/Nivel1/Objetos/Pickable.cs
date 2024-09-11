using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickable : Obj_Interactuable
{
    bool _pickedUp;
    public PickUp pickUpScript;

    private void Start()
    {
        //_camera = GameManager.Instance.Camera.transform;
        //_itemHolder = GameManager.Instance.ItemHolde;
        //pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        _speed = 7f;
    }

    private void Update()
    {
        if(_camera == null) _camera = GameManager.Instance.Camera.transform;
        if(_itemHolder == null) _itemHolder = GameManager.Instance.ItemHolde;
        if(pickUpScript == null) pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (holding && Input.GetMouseButtonDown(1))
        {
            Throw(pickUpScript._audioSource, pickUpScript.tirar);
            pickUpScript.sosteniendoBool = false;
            pickUpScript._audioSource.loop = false;
        }
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Movement(_itemHolder);
        }
    }
    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {

        if(pickUpScript.isHolding == false)
        {
            base.Interact(_audio, agarre, error);
            _rb.useGravity = false;
            canMove = true;
            _pickedUp = true;
            pickUpScript.isHolding = true;
            _col.enabled = false;
            _rb.constraints = RigidbodyConstraints.None;
            pickUpScript.esperaragarre = 0;
            _material = GetComponent<Renderer>().material;
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 0.4f);
        }


    }

    public override void Throw(AudioSource _audio, AudioClip arrojar)
    { 
        base.Throw(pickUpScript._audioSource, pickUpScript.tirar);
        _col.enabled = true;
        _pickedUp = false;
        pickUpScript.isHolding = false;
        onAir = true;
        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 1f);
        _audio.clip = arrojar;
        _audio.Play();

    }

    private void OnCollisionEnter(Collision collision)
    {
        Chocamiento choc = GetComponent<Chocamiento>();
        if (choc != null && onAir == true)
        {
            choc.Choco(transform.position);
            onAir = false;
        }
    }
}
