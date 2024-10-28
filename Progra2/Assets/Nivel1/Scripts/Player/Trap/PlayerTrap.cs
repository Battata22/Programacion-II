using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour, IInteractable
{
    CreatePlayerTrap _createTrapScript;
    CreateShadow _createShadowScript;

    [SerializeField] GameObject _mesh;

    //public delegate void VoidDelegate();
    public DelegateType.VoidDelegate myAction;
    bool canAct = true;
    float _cd;

    private void Awake()
    {
        myAction = delegate { };
        if (SelectorUI.habAct == 2)
        {
            gameObject.AddComponent<CreateShadow>();
            _createShadowScript = GetComponent<CreateShadow>();
        }

    }
    public void Initialize(CreatePlayerTrap newScript, DelegateType.VoidDelegate newAction, float newCD)
    {
        _createTrapScript = newScript;
        myAction = newAction;
        _cd = newCD;
    }

    public void Interact()
    {
        if (!canAct) return;
        StartCoroutine(SetInactive());
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

    private void OnTriggerEnter(Collider other)
    {
        var asus = other.gameObject.GetComponent<Asustable>();
        if (asus && asus._scared)
        {
            AsustableDetected(asus);
        }
    }

    void AsustableDetected(Asustable target)
    {
        print($"<color=#18f18F> Austable detectado </color>");
        // Trampa Activada +1
        Interact();
    }

    void Test()
    {
        print($"<color=#C8318F> Este mensaje es una prueba </color>");
    }

    IEnumerator SetInactive()
    {
        _mesh.SetActive(false);
        canAct = false;

        yield return new WaitForSeconds(_cd);

        _mesh.SetActive(true);
        canAct = true;
    }
}
