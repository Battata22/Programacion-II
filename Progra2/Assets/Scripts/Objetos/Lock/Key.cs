using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickable
{
    [Header("<color=yellow>Key</color>")]
    [SerializeField] protected int _myId;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Lock>(out var newLock) && newLock.id == _myId)
            newLock.Unlock();
        else
            base.OnCollisionEnter(collision);
    }

    public override void GetBlssed()
    {
        Debug.Log("<color=yellow> JAJA QUE BOLUDO INTENTO ENCANTAR LA LLAVE ¯|_(`-`)_/¯</color>");

    }

}
