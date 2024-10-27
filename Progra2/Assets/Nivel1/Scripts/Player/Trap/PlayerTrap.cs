using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour, IInteractable
{
    CreatePlayerTrap _createTrapScript;
    CreateShadow _createShadowScript;

    //public delegate void VoidDelegate();
    public DelegateType.VoidDelegate myAction;

    private void Awake()
    {
        myAction = delegate { };
        if (SelectorUI.habAct == 2)
        {
            gameObject.AddComponent<CreateShadow>();
            _createShadowScript = GetComponent<CreateShadow>();
        }

    }
    public void Initialize(CreatePlayerTrap newScript, DelegateType.VoidDelegate newAction)
    {
        _createTrapScript = newScript;
        myAction = newAction;
    }

    public void Interact()
    {
        //if (myAction == null)
        //{
        //    print("<color=red> AHHHHHHHHHHHHHHHHHHHHHHH </color>");
        //}
        //else
        if (_createShadowScript)
        {
            _createShadowScript.SpawnShadow();
        }
        else
            myAction();
    }

    void Test()
    {
        print($"<color=#C8318F> Este mensaje es una prueba </color>");
    }
}
