using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireflies : MonoBehaviour
{
    //Procrastinando, si señor
    [SerializeField] Transform _focusObject;
    [SerializeField] Vector3 _initialPos;
    [SerializeField] Player _player;
    [SerializeField] ParticleSystem _myParticleSys;
    [SerializeField] float _spd;
    [SerializeField] float _offset;
    [SerializeField] int _pulses;
    [SerializeField] public State _state { get;protected set; }

    [SerializeField] int _pulsesLeft;

    public event DelegateType.VoidDelegate OnPulseEnd = delegate { };
    private void Awake()
    {
        _initialPos = transform.position;
        _myParticleSys = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V) && _state == State.idle) 
        {
            ActivateMovement(false);
        }

        if(_state == State.idle)
        {
            IdleMovement();
        }
        if(_state == State.active) 
        {
            StartCoroutine(Movement());
        }

        if(_focusObject != null && _focusObject.position != _initialPos) 
        {
            _initialPos = _focusObject.position;
        }

    }

    public void ActivateMovement(bool destroyOnEnd)
    {
        _pulsesLeft = _pulses;
        _state = State.active;

        if (destroyOnEnd)
            OnPulseEnd += DestroyMe;

    }

    void DestroyMe()
    {
        Destroy(gameObject, 3f);
    }

    public void SetFocusObj(Transform newObj)
    {
        _myParticleSys.Stop();

        _focusObject = newObj;

        _myParticleSys.Play();
    }

    void StartIdle()
    {
        _myParticleSys.Stop();
        _myParticleSys.emissionRate = 10;

        //if (_focusObject != null)
        transform.position = _initialPos;
        //else 
        //    transform.position = _focusObject.position;
        _state = State.idle;
        _myParticleSys.Play();
    }

    void IdleMovement()
    {
        //
        transform.position = _initialPos + new Vector3(0,Mathf.Sin(Time.time) * _offset,0);
    }

    IEnumerator Movement()
    {
        Debug.Log("Entre a la corrutina");
        _state = State.inMovement;
        //_pulsesLeft = _pulses;

        _myParticleSys.Stop();
        transform.position = _player.transform.position + new Vector3(0,1,0);
        _myParticleSys.emissionRate = 50;
        _myParticleSys.Play();

        while(_state == State.inMovement && Vector3.SqrMagnitude(_initialPos-transform.position) > (0.5f * 0.5f)) 
        {
            var dir = (_initialPos - transform.position).normalized;
            transform.position += dir * _spd * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        _pulsesLeft--;
        if(_pulsesLeft <= 0)
        {
            OnPulseEnd();
            StartIdle();
        }
        else
        {
            _state = State.active;
        }
    }
    public enum State
    {
        idle,
        active,
        inMovement
    }

}
