using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Interactuable : MonoBehaviour 
{
    [SerializeField] protected Transform _camera;
    [SerializeField] protected float _cd; //de momento no usado
    [SerializeField] protected float _speed; //del objeto volando en tu direccion
    protected bool canMove = false, holding = false, onAir = false;
    [SerializeField] protected Transform _itemHolder; // punto al que va el objeto
    protected Rigidbody _rb;
    public bool mediano = false, grande = false;
    protected Collider _col;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    public virtual void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {
        _audio.clip = agarre;
        _audio.Play();
    }
    Vector3 dir = Vector3.zero;
    protected void Movement(Transform _target)
    {
        if(!holding) dir = (_target.position - transform.position);
        if (dir.sqrMagnitude > 0.2f)
        {
            transform.position += dir.normalized * _speed * Time.fixedDeltaTime;
        }
        else holding = true;

        if(holding)
        {
            transform.position = _target.position;
        }

    }

    public virtual void Throw(AudioSource _audio, AudioClip tirar)
    {
        if (holding == false)
        {
            return;
        }
        holding = false;
        canMove = false;
        _rb.useGravity = true;
        _rb.AddForce(_camera.forward * 50f, ForceMode.Impulse);
    }

    //public override void PlayMusic(AudioClip _audio1)
    //{
    //    base.PlayMusic(_audio1);
    //}

}
