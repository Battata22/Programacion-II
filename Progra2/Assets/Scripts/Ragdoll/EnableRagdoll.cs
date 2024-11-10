using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRagdoll : MonoBehaviour
{
    // este Script va en el NPC

    bool _ragdollActive;
    [SerializeField] RagdollBehaviour _ragdollPrefab;
    [SerializeField] float _forceMult;
    [SerializeField] bool _impulsePelvis;
    Rigidbody _rb;
    NPC _npc;
    ParticulasEfectos partEfectosScript;

    public delegate void Cosa();
    public Cosa Action;
    public event Cosa OnDeactivate = delegate { };


    private void Awake()
    {
        partEfectosScript = GetComponent<ParticulasEfectos>();
        Action = ActivateRagdoll;
        _rb = GetComponent<Rigidbody>();
        _npc = GetComponent<NPC>();
    }

    private void Update()
    {
        ////if (Input.GetKeyUp(KeyCode.P))
        ////{
        ////    Action();
        ////}
    }

    public void ActivateRagdoll()
    {
        print("<color=green> Ragdoll Activado </color>");

        var ragdoll = Instantiate(_ragdollPrefab, transform.position, Quaternion.identity);
        ragdoll.Initialize(this, transform.forward, _forceMult, _impulsePelvis);
        _npc.TurnOff();
        Action = DeactivateRagdoll;
    }

    public void ActivateRagdoll(Vector3 dir)
    {
        print("<color=green> Ragdoll Activado </color>");

        var ragdoll = Instantiate(_ragdollPrefab, transform.position, Quaternion.identity);
        ragdoll.Initialize(this, dir, _forceMult, _impulsePelvis);
        _npc.TurnOff();
        Action = DeactivateRagdoll;
    }

    public void DeactivateRagdoll()
    {

        print("<color=red> Ragdoll Desactivado </color>");

        _npc.TurnOn();
        OnDeactivate();

        //Action = ActivateRagdoll;
    }

}
