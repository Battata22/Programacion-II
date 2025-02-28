using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Chocotorta : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    Asustable _myAsus;
    PuzzleLvl3Cocina _cocinaPlz;

    Transform _followTarget;
    [SerializeField] Vector3 posTarget;
    [SerializeField] Vector3 localPosTarget;

    DelegateType.VoidDelegate MyMovement = delegate { };

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyMovement();
    }

    public void Initialize(Transform newTarget, Asustable newAsus, PuzzleLvl3Cocina newPlz)
    {
        _followTarget = newTarget;
        _myAsus = newAsus;
        _cocinaPlz = newPlz;

        var coliders = GetComponentsInChildren<Collider>();
        foreach (Collider colider in coliders)
        {
            colider.enabled = false;
        }

        _myAsus.OnRagdollTrigger += Throw;
        _myAsus.OnSlideStop += Throw;

        MyMovement = Movement;
    }

    void Movement()
    {
        posTarget = _followTarget.position;
        localPosTarget = _followTarget.localPosition;


        transform.position = _followTarget.position;
    }

    void Throw()
    {
        MyMovement = delegate { };

        var coliders = GetComponentsInChildren<Collider>();
        foreach (Collider colider in coliders)
        {
            colider.enabled = false;
        }

        _myAsus.OnRagdollTrigger -= Throw;
        _myAsus.OnSlideStop -= Throw;

        var x = Random.Range(0, 360);
        var y = Random.Range(0, 360);
        var z = Random.Range(0, 360);

        var dir = new Vector3(x,y,z).normalized;

        _rb.constraints = RigidbodyConstraints.None;
        
        _rb.AddForce(dir * 5f, ForceMode.Impulse);
    }
}
