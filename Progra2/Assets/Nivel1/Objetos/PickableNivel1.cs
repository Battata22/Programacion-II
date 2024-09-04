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
        if (canMove)
        {
            Movement(_itemHolder);
        }

        if(_pickedUp && Input.GetMouseButtonDown(0))
        {
            Throw();
        }
        
    }
    public override void Interact()
    {
        if(pickUpScript.isHolding == false)
        {
            _rb.useGravity = false;
            canMove = true;
            _pickedUp = true;
            pickUpScript.isHolding = true;
        }

    }

    void Throw()
    {
        holding = false;
        canMove = false;
        _pickedUp = false;
        _rb.useGravity = true;
        _rb.AddForce(_camera.forward * 50f, ForceMode.Impulse);
        pickUpScript.isHolding = false;
    }
}
