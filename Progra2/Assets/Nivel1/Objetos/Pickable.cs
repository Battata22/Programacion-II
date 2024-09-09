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
            //Debug.Log("audios");
            _rb.useGravity = false;
            //Debug.Log("gravedad");
            canMove = true;
            //Debug.Log("canmove");
            _pickedUp = true;
            //Debug.Log("agarrado");
            pickUpScript.isHolding = true;
            //Debug.Log("algo");
        }

    }

    public override void Throw(AudioSource _audio, AudioClip arrojar)
    { 
        base.Throw(pickUpScript._audioSource, pickUpScript.tirar);
        _pickedUp = false;
        pickUpScript.isHolding = false;
        _audio.clip = arrojar;
        _audio.Play();

    }
}
