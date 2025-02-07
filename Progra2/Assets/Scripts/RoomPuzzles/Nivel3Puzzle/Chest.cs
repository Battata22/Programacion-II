using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ILockeable
{
    //idea
    // ta bloqueado
    // se abre y dentro hay objetos
    // nig...

    [SerializeField] Disco _discoPrefab;

    [SerializeField] bool _locked;
    public bool locked { get { return _locked; } }

    event DelegateType.VoidDelegate OnChestOpen = delegate { };
    event DelegateType.VoidDelegate OnChestClose = delegate { };

    public void Lock()
    {
        Debug.Log("<color=red> Toy cershado ura</color>");


    }

    public void Unlock()
    {
        Debug.Log("<color=green>ah</color>");

        //cambia el mesh
        //crear disco choto
        var disco = Instantiate(_discoPrefab,transform.position + new Vector3(0f,1f,0f), Quaternion.identity);

    }


}
