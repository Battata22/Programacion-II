using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour, IInteractable
{
    CreatePlayerTrap _createTrapScript;

    //public delegate void VoidDelegate();
    public DelegateType.VoidDelegate myAction;

    private void Awake()
    {
        myAction = delegate { };
    }
    public void Initialize(CreatePlayerTrap newScript, DelegateType.VoidDelegate newAction)
    {
        _createTrapScript = newScript;
        myAction = newAction;
    }

    public void Interact()
    {
        if (myAction == null)
        {
            print("<color=red> AHHHHHHHHHHHHHHHHHHHHHHH </color>");
        }
        else
            myAction();
    }

    void Test()
    {
        print($"<color=#C8318F> Este mensaje es una prueba </color>");
    }
}
