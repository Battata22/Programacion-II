using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ghostbuster : NPC
{
    [Header("<color=red> Ghostbuster </color>")]
    [SerializeField] GB_FOV _gbFov;
    [SerializeField] float _torque, _angerRange, _angerTime , _attackRange, _suctionForce, _attackDuration, _atkDelay, _attackCD, _killRange;
    float _waitAnger, _lastAttack = -1;

    [SerializeField] Player _target;

    bool _lastState;
    [SerializeField] bool _angry, _isAttacking, _canAttack;
    bool _activeChase = false;

    protected override void Start()
    {
        base.Start();
        //_target = GameManager.Instance.Player;
        _gbFov = GetComponent<GB_FOV>();
    }

    private void Update()
    {
        if (!_AIActive) return;
        if(_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();

        if ((!_doubt && !_angry &&Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            //Debug.Log("<color=#26c5f0> LLege al destino </color>");

            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);
        }

        if (_doubt)
            _searchingTimer += Time.deltaTime;
        if (_searchingTimer > 12f) StopSearching();
        if (_inPlace) _waitDoubt += Time.deltaTime;

        if (_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist))
        {
            _agent.speed = 0;

            if (!_inPlace)
            {
                StartSearching();
            }
        }
        if (_doubt && _inPlace && _waitDoubt >= 2)
        {           
            StopSearching();
        }

        if (_gbFov.hasLOS != _lastState)
        {
            _lastState = _gbFov.hasLOS;
            if (_gbFov.hasLOS && !_activeChase && !_isAttacking && !_target.underAttack)
            {
                //Debug.Log("Te veo");
                GetAngry();
            }           
        }

        if(_angry && !_isAttacking && Time.time - _waitAnger > _angerTime)
        {
            StopAnger();
            //Debug.Log("<color=red> Despues de stop </color>");
            _agent.SetDestination(GetNewNode(_actualNode).position);
            //Debug.Log("<color=green> Despues de set destination </color>");
        }
        if (!_isAttacking && !_target.underAttack && _canAttack &&_gbFov.hasLOS && Vector3.SqrMagnitude(transform.position - _target.transform.position) <= (_attackRange * _attackRange))
        {
            StartCoroutine(DelayAttack());
        }

        if (!_canAttack && Time.time - _lastAttack > _attackCD) _canAttack = true;
        
    }

    private void FixedUpdate()
    {
        if (_isAttacking) 
        {
            Attack();
            RotateToTarget();
        }
    }

    public override void GetDoubt(Vector3 pos)
    {
        //activar duda
        Debug.Log("<color=yellow> Escuche algo </color>");

        if (_angry) return;
        //Debug.Log("Duda de asustable");

        _doubt = true;

        //_audioSource.clip = doubtClip;
        //_audioSource.Play();

        _agent.speed = speedDoubt;

        _agent.SetDestination(pos);
        _searchingPos = pos;
    }

    public override void GetScare()
    {
        //Activar Anger supongo
        Debug.Log("<color=red> YA TE VOY A AGARRAR </color>");
        GetAngry()  ;
    }

    void GetAngry()
    {

        _angry = true;
        _waitAnger = Time.time;
        StartCoroutine(ChaseTarget());
    }

    void StopAnger()
    {
        _angry = false;
        //StopCoroutine(ChaseTarget());
        
    }

    void StartAttack()
    {
        //Activar modo Luigi
        //Los objetos libianos cercanos tambien seria succionados? idea
        //Debug.Log("Iniciando Ataque");
        _target.underAttack = true;
        _target.attacker = this;
    }

    void Attack()
    {
        Vector3 direction = _target.transform.position - transform.position;
        transform.Rotate(direction);
        if (Vector3.SqrMagnitude(direction) <= (_killRange * _killRange))
        {
            _target.GetDamage();
            EndAttack();
        }
        if ( Vector3.Angle(transform.forward, direction) < 30f)
        {
            _target.ApplyForce(-direction, _suctionForce);
        }
        else
        {
            _target.ApplyForce(-_target.transform.forward, 0);
        }
    }

    void EndAttack()
    {
        //if(!_isAttacking) return;
        Debug.Log("Terminando Ataque");
        _target.underAttack = false;
        _target.attacker = null;
        _target.ApplyForce(new Vector3(), 0);
        _canAttack = false;
        _isAttacking = false;
        _angry = false;
        _lastAttack = Time.time;

        _agent.speed = speedNormal;
    }

    void RotateToTarget()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, direction) <= 90f       )
        {
            if(Vector3.Angle(transform.right, direction) <= 90f)
            {
                Debug.Log("Giro Horario");
                transform.Rotate(0f, _torque * Time.fixedDeltaTime, 0f);
            }
            else
            {
                Debug.Log("Giro Antihorario");
                transform.Rotate(0f, -_torque * Time.fixedDeltaTime, 0f);
            }
        }
        else
        {
            if (Vector3.Angle(transform.right, direction) <= 90f)
            {
                Debug.Log("Giro Horario");
                transform.Rotate(0f, _torque * Time.fixedDeltaTime, 0f);
            }
            else
            {
                Debug.Log("Giro Antihorario");
                transform.Rotate(0f, -_torque * Time.fixedDeltaTime, 0f);
            }
        }
        
    }

    private IEnumerator ChaseTarget()
    {
        _activeChase = true;
        //Debug.Log("<color=#825aef>Inicia Cazeria</color>");
        //consigue pos de Gus cada medio segundo
        
        //https://www.youtube.com/watch?v=5T5BY1j2MkE no abrir

        //if(!_angry) yield return null;
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (_angry)
        {
            yield return wait;
            Debug.Log("<color=red>Cazando</color>");
            _actualNode = _target.transform;
            _agent.SetDestination(_actualNode.position  );
        }

        if (!_isAttacking)
        {
            //Debug.Log("<color=#ef5ae4>Termina Cazeria</color>");
            _actualNode = GetNewNode(_actualNode);
            _agent.SetDestination(_actualNode.position);
        }
        _activeChase = false;
        //yield return null;       
    }

    private IEnumerator DelayAttack()
    {
        _isAttacking = true;
        _agent.speed = 0f;
        Debug.Log("Preparando ataque");

        yield return new WaitForSeconds(_atkDelay);
        StartAttack();

        yield return new WaitForSeconds(_attackDuration);
        EndAttack();
    }
}
