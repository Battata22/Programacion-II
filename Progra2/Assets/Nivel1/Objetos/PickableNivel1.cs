using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableNivel1 : Obj_InteractuableNivel1
{
    bool _pickedUp;

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
        _rb.useGravity = false;
        canMove = true;    
        _pickedUp = true;
    }

    void Throw()
    {
        holding = false;
        canMove = false;
        _pickedUp = false;
        _rb.useGravity = true;
        _rb.AddForce(_camera.forward * 50f, ForceMode.Impulse);
    }
}
