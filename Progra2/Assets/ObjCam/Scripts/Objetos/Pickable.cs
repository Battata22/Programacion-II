using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Obj_Interactuable
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
        canMove = false;
        _pickedUp = false;
        _rb.useGravity = true;
        _rb.AddForce(_camera.forward * 50f, ForceMode.Impulse);
    }
}
