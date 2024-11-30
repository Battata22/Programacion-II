using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectTrap : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject _mesh, icono;

    public delegate void VoidDelegate();
    public event VoidDelegate OnTrapActive = delegate { };

    public DelegateType.VoidDelegateTrans myAction;
    bool canAct = true;
    float _cd;
    SpriteRenderer _icono;
    [SerializeField] Transform _lookingAt;

    private void Start()
    {
        _icono = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (_lookingAt == null) _lookingAt = GameManager.Instance.Camera.transform;

        icono.transform.LookAt(_lookingAt.position);
    }

    public void Initialize(DelegateType.VoidDelegateTrans newAction, float newCD)
    {
        //_createTrapScript = newScript;
        myAction = newAction;
        _cd = newCD;

        canAct = true;
    }
    public void Interact()
    {
        //Debug.Log("Entro a Interact");  
        if (!canAct) return;
        //Debug.Log("Antes de Set Inactive");
        StartCoroutine(SetInactive());
        //Debug.Log("Antes de Action");
        myAction(transform);
        //Debug.Log("Antes de OnTrapActive");
        OnTrapActive();
    }
    IEnumerator SetInactive()
    {
        _mesh.SetActive(false);
        canAct = false;
        //_icono.sprite = null;

        //Debug.Log($"Inactive for {_cd} seconds");

        yield return new WaitForSeconds(_cd);

        _mesh.SetActive(true);
        canAct = true;
    }
}
