using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Ghostbuster : NPC
{
    [Header("<color=red> Ghostbuster </color>")]
    [SerializeField] GB_FOV _gbFov;
    [SerializeField] float _torque, _angerRange, _angerTime , _attackRange, _suctionForce, _attackDuration, _atkDelay, _attackCD, _killRange;
    public float _waitAnger, _lastAttack = -1, waitTrampa, waitTrampaRandom;

    [SerializeField] Player _target;
    [SerializeField] TrampaGB trampaPrefab;

    bool _lastState;
    [SerializeField] bool _angry, _isAttacking, _canAttack;
    [SerializeField] AudioClip _clipAspiradora, doubtClip, _clipAngry;
    bool _activeChase = false, _startingAttack, _firstAnger = false;

    [SerializeField] int maxTraps;
    [SerializeField] int currentTraps;

    ParticleSystem _particleGen;

    protected override void Start()
    {
        //_agent.speed;
        base.Start();
        waitTrampaRandom = Random.Range(5, 101);
        //_target = GameManager.Instance.Player;
        _gbFov = GetComponent<GB_FOV>();
        _particleGen = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {

        if (_firstAnger == true)
        {
            waitTrampa += Time.deltaTime;
            if (waitTrampa >= waitTrampaRandom)
            {
                PutTrap(transform);
            }
        }

        if (!_AIActive) return;
        if(_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();

        if (!_canAttack && Time.time - _lastAttack > _attackCD)
        {
            _canAttack = true;
            _agent.speed = speedNormal;
            _actualNode = GetNewNode(_actualNode);
            _agent.SetDestination(_actualNode.position);
            return;
        }

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
        if (!_isAttacking && !_target.underAttack && _canAttack && _gbFov.hasLOS && !_startingAttack &&Vector3.SqrMagnitude(transform.position - _target.transform.position) <= (_attackRange * _attackRange))
        {
            StartCoroutine(DelayAttack());
        }

        
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
        //Debug.Log("<color=yellow> Escuche algo </color>");

        if (_angry) return;
        if (_isAttacking) return;
        if (!_canAttack) return;
        //Debug.Log("Duda de asustable");

        _doubt = true;

        _audioSource.clip = doubtClip;
        _audioSource.Play();

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
        _firstAnger = true;
        if (!_canAttack) return;
        if(_isAttacking) return;
        _audioSource.clip = _clipAngry;
        _audioSource.Play();
        //print("alakazam");
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
        if (!_canAttack) return;
        _startingAttack = false;
        _particleGen.Play();
        //Activar modo Luigi
        //Los objetos libianos cercanos tambien seria succionados? idea
        //Debug.Log("Iniciando Ataque");
        _isAttacking = true;
        _angry=false;   
        _agent.speed = 0f;
        _target.underAttack = true;
        _target.attacker = this;
        _audioSource.clip = _clipAspiradora;
        _audioSource.Play();
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
        //Debug.Log("Terminando Ataque");
        //_agent.speed = speedNormal;
        _particleGen.Stop();
        _target.underAttack = false;
        _target.attacker = null;
        _target.ApplyForce(new Vector3(), 0);
        _canAttack = false;
        _isAttacking = false;
        _angry = false;
        _lastAttack = Time.time;
        _audioSource.Stop();

        //_agent.speed = speedNormal;
    }

    void RotateToTarget()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, direction) <= 90f       )
        {
            if(Vector3.Angle(transform.right, direction) <= 90f)
            {
                //Debug.Log("Giro Horario");
                transform.Rotate(0f, _torque * Time.fixedDeltaTime, 0f);
            }
            else
            {
                //ebug.Log("Giro Antihorario");
                transform.Rotate(0f, -_torque * Time.fixedDeltaTime, 0f);
            }
        }
        else
        {
            if (Vector3.Angle(transform.right, direction) <= 90f)
            {
                //Debug.Log("Giro Horario");
                transform.Rotate(0f, _torque * Time.fixedDeltaTime, 0f);
            }
            else
            {
                //Debug.Log("Giro Antihorario");
                transform.Rotate(0f, -_torque * Time.fixedDeltaTime, 0f);
            }
        }
        
    }

    void PutTrap(Transform lugar)
    {
        if(currentTraps < maxTraps)
        {
            currentTraps++;
            waitTrampa = 0;
            waitTrampaRandom = Random.Range(15, 121);
            var newTrap = Instantiate(trampaPrefab, lugar.position, Quaternion.identity);
            newTrap.Initialize(this);
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

        while (_angry && _canAttack)
        {
            yield return wait;
            Debug.Log("<color=red>Cazando</color>");
            _actualNode = _target.transform;
            _agent.SetDestination(_actualNode.position  );
        }

        if (!_isAttacking && _canAttack)
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
        _agent.speed = 0f;
        //Debug.Log("Preparando ataque");
        _startingAttack =true;
        yield return new WaitForSeconds(_atkDelay);
        //_isAttacking = true;
        //_particleGen.gameObject.SetActive(true);
        StartAttack();

        yield return new WaitForSeconds(_attackDuration);
        
        EndAttack();
    }

    //protected override Transform GetNewNode(Transform lastNode = null)
    //{
    //    if (!_canAttack) return null;            
    //    return base.GetNewNode(lastNode);
    //}
}
