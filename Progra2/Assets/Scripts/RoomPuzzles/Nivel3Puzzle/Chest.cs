using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ILockeable
{
    //idea
    // ta bloqueado
    // se abre y dentro hay objetos
    // nig...

    [SerializeField] bool _locked;
    public bool locked { get { return _locked; } }

    event DelegateType.VoidDelegate OnChestOpen = delegate { };
    event DelegateType.VoidDelegate OnChestClose = delegate { };

    public void Lock()
    {
        throw new System.NotImplementedException();
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
    }


}
