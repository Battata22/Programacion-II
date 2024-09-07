using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableNivel1 : Obj_InteractuableNivel1
{
    bool _pickedUp;
    public PickUpNivel1 pickUpScript;

    private void Start()
    {
        
    }

    private void Update()
    {

        if(holding && Input.GetMouseButtonDown(1))
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
            _rb.useGravity = false;
            canMove = true;
            _pickedUp = true;
            pickUpScript.isHolding = true;
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
