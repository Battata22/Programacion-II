using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_InteractuableNivel1 : MonoBehaviour 
{
    [SerializeField] protected Transform _camera;
    [SerializeField] protected float _cd; //de momento no usado
    [SerializeField] protected float _speed; //del objeto volando en tu direccion
    protected bool canMove = false, holding = false;
    [SerializeField] protected Transform _itemHolder; // punto al que va el objeto
    protected Rigidbody _rb;
    public bool _sonoro = false;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {
        _audio.clip = agarre;
        _audio.Play();
    }

    protected void Movement(Transform _target)
    {
        if((_target.position.x - transform.position.x) >= 0.2 || (_target.position.x - transform.position.x) <= 0 && (_target.position.z - transform.position.z) >= 0.2 || (_target.position.z - transform.position.z) <= 0)
        {
            Vector3 dir = (_target.position - transform.position).normalized;
            transform.position += dir * _speed * Time.fixedDeltaTime;
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
