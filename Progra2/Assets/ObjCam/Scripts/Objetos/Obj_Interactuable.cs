using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Interactuable : MonoBehaviour
{
    [SerializeField] protected Transform _camera;
    [SerializeField] protected float _cd; //de momento no usado
    [SerializeField] protected float _speed; //del objeto volando en tu direccion
    protected bool canMove = false;
    [SerializeField] protected Transform _itemHolder; // punto al que va el objeto
    protected Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void Interact()
    {

    }

    protected void Movement(Transform _target)
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        transform.position += dir * _speed * Time.deltaTime;
    }

}
