using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Ghostbuster : NPC
{
    [Header("<color=red> Ghostbuster </color>")]
    [SerializeField] GB_FOV _gbFov;
    [SerializeField] float _torque, _angerRange, _angerTime , _attackRange, _suctionForce, _attackDuration, _atkDelay, _attackCD, _killRange, _shadowFightTime;
    public float _waitAnger, _lastAttack = -1, _waitTrampa, _waitTrampaRandom, _waitKill;

    [SerializeField] Player _target;
    [SerializeField] TrampaGB trampaPrefab;

    bool _lastState;
    [SerializeField] bool _angry, _isAttacking, _canAttack, _fighting = false   ;
    [SerializeField] AudioClip _clipAspiradora, doubtClip, _clipAngry;
    bool _activeChase = false, _startingAttack, _firstAnger = false;

    [SerializeField] Animator _anim;
    //[SerializeField]bool idle = false, walking, running = false, aiming = false;

    [SerializeField] int maxTraps;
    [SerializeField] int currentTraps;

    ParticleSystem[] _parGens; 
    ParticleSystem _tornadoGen;
    ParticleSystem _smokeGen;

    protected override void Start()
    {
        //_agent.speed;
        base.Start();
        _waitTrampaRandom = Random.Range(5, 101);
        //_target = GameManager.Instance.Player;
        _gbFov = GetComponent<GB_FOV>();
        _parGens = GetComponentsInChildren<ParticleSystem>();
        _tornadoGen = _parGens[1];
        _smokeGen = _parGens[0];
        _anim = GetComponentInChildren<Animator>();
        _anim.SetBool("Walking", true);
    }

    private void Update()
    {

        if (!_AIActive) return;
        if(_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();
        if (_fighting) return;
        if (!_canAttack && Time.time - _lastAttack > _attackCD)
        {
            _canAttack = true;
            //_agent.speed = speedNormal;
            SetSpeed();
            _actualNode = GetNewNode(_actualNode);
            _agent.SetDestination(_actualNode.position);
            _anim.SetBool("Idle", false);
            _anim.SetFloat("zAxis", 0f);
            _anim.SetBool("Walking", true);
            return;
        }

        if (_firstAnger == true)
        {
            _waitTrampa += Time.deltaTime;
            if (_waitTrampa >= _waitTrampaRandom)
            {
                PutTrap(transform);
            }
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
            //_agent.speed = 0;

            if (!_inPlace)
            {
                StartSearching();
                SetSpeed();
                _anim.SetBool("Idle", true);
                _anim.SetBool("Walking", false);
            }
        }
        if (_doubt && _inPlace && _waitDoubt >= 2)
        {
            _anim.SetBool("Idle", false);
            _anim.SetBool("Walking", true);
            StopSearching();
            
            if(!_angry)_anim.SetFloat("zAxis", 0);
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
        if (!_isAttacking && !_target.underAttack && _canAttack && /*_gbFov.hasLOS &&*/ !_startingAttack &&Vector3.SqrMagnitude(transform.position - _target.transform.position) <= (_attackRange * _attackRange))
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
        if (_fighting) return;
        _anim.SetFloat("zAxis", 1);
        //Debug.Log("Duda de asustable");

        _doubt = true;

        _audioSource.clip = doubtClip;
        _audioSource.Play();

        //_agent.speed = speedDoubt;
        SetSpeed();

        _agent.SetDestination(pos);
        _searchingPos = pos;
    }

    public override void GetScare()
    {

        //Activar Anger supongo
        //Debug.Log("<color=red> YA TE VOY A AGARRAR </color>");
        GetAngry();
    }

    void GetAngry()
    {
        if (!_canAttack) return;
        if(_isAttacking) return;
        if (_fighting) return;
        _firstAnger = true;
        _anim.SetFloat("zAxis", 1);
        _audioSource.clip = _clipAngry;
        _audioSource.Play();
        StopSearching();
        //print("alakazam");
        _angry = true;
        SetSpeed();
        _waitAnger = Time.time;
        StartCoroutine(ChaseTarget());
    }

    void StopAnger()
    {
        _anim.SetFloat("zAxis", 0);
        _angry = false;
        SetSpeed();
        //StopCoroutine(ChaseTarget());

    }

    
    void StartAttack()
    {
        if (!_canAttack) return;
        _anim.SetBool("Idle", false);
        _anim.SetBool("Walking", false);
        _anim.SetBool("Attacking", true);
        _startingAttack = false;
        _tornadoGen.Play();
        //Activar modo Luigi
        //Los objetos libianos cercanos tambien seria succionados? idea
        //Debug.Log("Iniciando Ataque");
        _isAttacking = true;
        _angry=false;
        //_agent.speed = 0f;
        StopSearching();
        SetSpeed();
        _target.underAttack = true;
        _target.attacker = this;
        _audioSource.clip = _clipAspiradora;
        _audioSource.Play();
        _waitKill = Time.time;
    }

    void Attack()
    {
        if(_target.scapeSpam >=30)
            EndAttack();
        Vector3 direction = _target.transform.position - transform.position;
        transform.Rotate(direction);
        _target.ApplyForce(-direction, _suctionForce);
        if (Vector3.SqrMagnitude(direction) <= (_killRange * _killRange) && Time.time - _waitKill > _atkDelay)
        {
            _target.GetDamage();
            EndAttack();
        }
        //if ( Vector3.Angle(transform.forward, direction) < 30f)
        //{
        //    _target.ApplyForce(-direction, _suctionForce);
        //}
        //else
        //{
        //    _target.ApplyForce(-_target.transform.forward, 0);
        //}
    }

    void EndAttack()
    {
        //if(!_isAttacking) return;
        //Debug.Log("Terminando Ataque");
        //_agent.speed = speedNormal;
        _anim.SetFloat("zAxis", 0);
        _anim.SetBool("Attacking", false);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Walking", false);
        _tornadoGen.Stop();
        _target.underAttack = false;
        _target.attacker = null;
        _target.ApplyForce(new Vector3(), 0);
        _canAttack = false;
        _isAttacking = false;
        _angry = false;
        _lastAttack = Time.time;
        //_agent.speed = 0;
        SetSpeed();
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
            _waitTrampa = 0;
            _waitTrampaRandom = Random.Range(15, 121);
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
            //Debug.Log("<color=red>Cazando</color>");  
            _actualNode = _target.transform;
            _agent.SetDestination(_actualNode.position  );
            yield return wait;
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
        _startingAttack =true;
        //_agent.speed = 0f;
        SetSpeed();
        //Debug.Log("Preparando ataque");
        yield return new WaitForSeconds(_atkDelay);
        //_isAttacking = true;
        //_particleGen.gameObject.SetActive(true);
        StartAttack();

        //yield return new WaitForSeconds(_attackDuration);
        
        //EndAttack();
    }

    //protected override Transform GetNewNode(Transform lastNode = null)
    //{
    //    if (!_canAttack) return null;            
    //    return base.GetNewNode(lastNode);
    //}

    public void AttackShadow(GameObject shadow)
    {
        if (_angry) return;
        if (!_canAttack) return;
        _fighting = true;
        //_agent.speed = 0f;
        StopSearching();
        SetSpeed();
        _smokeGen.Play();
        Destroy(shadow);

        _anim.SetBool("Idle", true);
        _anim.SetFloat("zAxis", 0f);
        _anim.SetBool("Walking", false);

        StartCoroutine(StopFight(_shadowFightTime));
    }

    IEnumerator StopFight(float num)
    {
        WaitForSeconds wait = new WaitForSeconds(num);
        yield return wait;

        //_agent.speed = speedNormal;
        if (_fighting)
        {
            _fighting = false;
            SetSpeed();
            _smokeGen.Stop();
            _anim.SetBool("Idle", false);
            _anim.SetFloat("zAxis", 0f);
            _anim.SetBool("Walking", true);
        }
    }

    void SetSpeed()
    {
        if(_angry)
        {
            _agent.speed = speedScared;
        }
        else if (_doubt)
        {
            _agent.speed = speedDoubt;
        }
        else if (_fighting)
        {
            _agent.speed = 0;
        }
        else if (_canAttack)
        {
            _agent.speed = speedNormal;
        }
        if (_fighting || _isAttacking || !_canAttack || _startingAttack || _inPlace)
        {
            _agent.speed = 0f;
        }
    }
}
