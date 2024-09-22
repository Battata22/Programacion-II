using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Interactuable : MonoBehaviour 
{
    [SerializeField] protected Transform _camera;
    [SerializeField] protected float _cd, _lastInteract; //de momento no usado
    [SerializeField] protected float _speed; //del objeto volando en tu direccion
    [SerializeField] protected Transform _itemHolder; // punto al que va el objeto
    protected Rigidbody _rb;
    public bool mediano = false, grande = false, holding = false;
    protected bool _canMove = false, _onAir = false;
    protected Collider _col;
    [SerializeField] protected Material _materialNormal, _materialFade;
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected LayerMask _dropLayers;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        
        //_materialNormal = GetComponent<Material>(); 
    }

    public virtual void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {
        _lastInteract = Time.time;
        _audio.clip = agarre;   
        _audio.PlayOneShot(agarre);
    }
    Vector3 dir = Vector3.zero;
    protected void Movement(Transform _target)
    {
        if(!holding) dir = (_target.position - transform.position);
        if (dir.sqrMagnitude > 0.2f)
        {
            transform.position += dir.normalized * _speed * Time.fixedDeltaTime;
        }
        else
        {

            GameManager.Instance.HandState.holding = true;
            GameManager.Instance.HandState.relax = false;
            GameManager.Instance.HandState.ChangeState();

            holding = true;
        }


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
        _lastInteract = Time.time;

        GameManager.Instance.HandState.holding = false;
        GameManager.Instance.HandState.pointing = false;
        GameManager.Instance.HandState.relax = true;
        GameManager.Instance.HandState.ChangeState();

        holding = false;
        _canMove = false;
        _rb.useGravity = true;
        _rb.AddForce(_camera.forward * 50f, ForceMode.Impulse);
    }

    //public override void PlayMusic(AudioClip _audio1)
    //{
    //    base.PlayMusic(_audio1);
    //}

}
