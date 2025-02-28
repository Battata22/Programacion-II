using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
//using static UnityEditor.PlayerSettings;

public class Ghostbuster : NPC , ICanSlide, IRagdoll
{
    [Header("<color=red> Ghostbuster </color>")]
    [SerializeField] GB_FOV _gbFov;
    [SerializeField] float _torque, _angerRange, _angerTime , _attackRange, _suctionForce, _attackDuration, _atkDelay, _attackCD, _killRange, _shadowFightTime;
    [SerializeField] int _spamScape;
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
    [SerializeField] int minTrapTime;
    [SerializeField] int currentTraps;
    [SerializeField] AudioSource _sourceDamage;

    public delegate void EventVoid();
    public event EventVoid OnAttackStart, OnAttackEnd;

    ParticleSystem[] _parGens; 
    ParticleSystem _tornadoGen;
    ParticleSystem _smokeGen;

    Rigidbody _rb;
    bool _sliding;

    public event DelegateType.VoidDelegate OnSlideStop = delegate { };

    //Ragdoll
    public event DelegateType.VoidDelegate OnRagdollTrigger = delegate { };

    [SerializeField] GameObject _mesh;

    EnableRagdoll _myRagdollSwitch;


    [Header("<color=green> Ragdoll </color>")]
    [SerializeField] public bool canRagdoll = true;
    [SerializeField, Tooltip("<color=red> DON'T TOUCH, ONLY TO READ </color>")]bool inRagdoll = false;

    [Header("<color=yellow> Gadgets </color>")]
    [SerializeField] GB_GadgetSpawner _GadgetSpawner;
    [SerializeField] Transform _pulseOrigin;
    [SerializeField] float _repairTime;
    [SerializeField] float _timeBtRepairs;
    [SerializeField] float _spawnGadgetWait;

    List<GB_Gadget> _activeGadgets = new();
    List<GB_Gadget> _brokenGadgets = new();

    float _lastRepairCall = 0;
    float _lastGadgetSpwTime = 0;
    bool _inRepairPos = false;
    bool _isTringToRepair = false;
    bool _hasObjToRepair = false;

    bool _lookingActive = false;


    protected void Awake()
    {
        _myRagdollSwitch = GetComponent<EnableRagdoll>();
        _lastGadgetSpwTime = Time.time;

    }

    //de alguna forma esto crashea unity ;p
    //private void OnEnable()
    //{
    //    //_lastGadgetSpwTime = Time.time - _spawnGadgetWait * 0.3f;
    //}

    protected override void Start()
    {
        //_agent.speed;
        base.Start();
        //yield return null;
        GameManager.Instance.Gb.Add(this);
        _waitTrampaRandom = Random.Range(5, 101);
        //_target = GameManager.Instance.Player;
        _gbFov = GetComponent<GB_FOV>();
        _parGens = GetComponentsInChildren<ParticleSystem>();
        _rb = GetComponent<Rigidbody>();
        _tornadoGen = _parGens[1];
        _smokeGen = _parGens[0];
        _anim = GetComponentInChildren<Animator>();
        _anim.SetBool("Walking", true);
        if (_actualNode != null)
            _agent.SetDestination(_actualNode.position);
        foreach(var trap in GameManager.Instance.PlayerTraps)
        {
            trap.OnTrapActive += GetAngry;
        }

        _lastGadgetSpwTime = Time.time;
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
            //_actualNode = GetNewNode(_actualNode);
            //_agent.SetDestination(_actualNode.position);
            SetNewDestination(_actualNode);

            _anim.SetBool("Idle", false);
            _anim.SetFloat("zAxis", 0f);
            _anim.SetBool("Walking", true);
            return;
        }

        //Rework, reparar objetos
        if (_hasObjToRepair && Time.time - _lastRepairCall > _timeBtRepairs)
        {
            GoRepairGadget();
            _lastRepairCall = Time.time;
        }

        if (_firstAnger == true)
        {
            _waitTrampa += Time.deltaTime;
            if (_waitTrampa >= _waitTrampaRandom && _canAttack && !_isAttacking)
            {
                PutTrap(transform);
            }
        }

        if(Time.time - _lastGadgetSpwTime > _spawnGadgetWait)
        {
            _lastGadgetSpwTime = Time.time;

            //1 para la cam, se pone a mano en el gb_gadgetSpawner
            //_GadgetSpawner.SpawnGadget(transform, 1, this);
            if (!_isAttacking && _canAttack)
                _GadgetSpawner.SpawnRandomGadget(transform, this);
            //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa aa aa a agria
        }

        if ((!_doubt && !_lookingActive && !_angry && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            StartCoroutine(LookAround());
        }

        //REEMPLAZADO POR EL IF DE ARRIBA
        //if ((!_doubt && !_angry &&Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        //{
        //    //Debug.Log("<color=#26c5f0> LLege al destino </color>");

        //    _actualNode = GetNewNode(_actualNode);

        //    _agent.SetDestination(_actualNode.position);
        //}

        if (_doubt)
            _searchingTimer += Time.deltaTime;
        if (_searchingTimer > 12f) StopSearching();
        if (_inPlace) _waitDoubt += Time.deltaTime;

        if (_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist * 1.5f))
        {
            //_agent.speed = 0;

            if (!_inPlace)
            {
                StartSearching();
                SetSpeed();

                _GadgetSpawner.SpawnGadget(_pulseOrigin, 0, this);//pulso  scaner

                _anim.SetBool("Idle", true);
                _anim.SetBool("Walking", false);
            }
        }

        //Rework repair object
        if (_isTringToRepair && !_inRepairPos && Vector3.SqrMagnitude(transform.position - _brokenGadgets[0].transform.position) <= (_changeNodeDist * _changeNodeDist * 4))
        {
            _inRepairPos = true;

            //Debug.Log($"<color=yellow> Llamando corrutina reparar </color>");

            StartCoroutine(StartRepairGadget(_repairTime));
        }
        else if (_inRepairPos)
        {
            //Debug.Log($"<color=yellow> Estoy del otro lado del if </color>");

            _inRepairPos = false;
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
            if (_gbFov.hasLOS && !_activeChase && !_sliding && !_isAttacking && !_target.underAttack)
            {
                //Debug.Log("Te veo");
                GetAngry();

                //GetScared(1);
            }           
        }

        if (_angry && !_isAttacking && Time.time - _waitAnger > _angerTime)
        {
            StopAnger();
            //Debug.Log("<color=red> Despues de stop </color>");
            _agent.SetDestination(GetNewNode(_actualNode).position);
            //Debug.Log("<color=green> Despues de set destination </color>");
        }
        if (!_isAttacking && !_target.underAttack && _canAttack && !_target.possessing && !_sliding && _gbFov.hasLOS && _angry
            && /*_gbFov.hasLOS &&*/ !_startingAttack &&Vector3.SqrMagnitude(transform.position - _target.transform.position) <= (_attackRange * _attackRange))
        {
            StartCoroutine(DelayAttack());
        }

        if (_sliding && Time.time - lastSlide > minSlideTime && _rb.velocity.sqrMagnitude < 0.5f * 0.5f)
        {
            StopSlide();
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

    public override void GetDoubt(Vector3 pos, int g)
    {
        //GetAngry();

        //activar duda
        //Debug.Log("<color=yellow> Escuche algo </color>");
        if (_angry) return;
        if (_isAttacking) return;
        if (!_canAttack) return;
        if (_fighting) return;
        if (inRagdoll) return;
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

    public override void GetScared(float scareAmount, int roomIndex ,Transform a = null)
    {

        //Activar Anger supongo
        //Debug.Log("<color=red> YA TE VOY A AGARRAR </color>");

        //GetAngry();

        //Rework Tirar Flashbang

        _GadgetSpawner.SpawnGadget(_pulseOrigin, 0, this);
    }

    public void GetAngry()
    {
        if (!_canAttack) return;
        if(_isAttacking) return;
        if (_fighting) return;
        if (_angry) return;
        _firstAnger = true;
        _anim.SetFloat("zAxis", 1);
        _audioSource.clip = _clipAngry;
        _audioSource.Play();
        StopSearching();
        //print("alakazam");
        _angry = true;
        SetSpeed();
        _waitAnger = Time.time;
        StartCoroutine(ChaseTarget(0.5f));
    }

    void StopAnger()
    {
        _anim.SetFloat("zAxis", 0);
        _angry = false;
        SetSpeed();
        //StopCoroutine(ChaseTarget());

        ChaseTarget(0);
    }

    
    void StartAttack()
    {
        if (!_canAttack) return;
        if(OnAttackStart != null)
            OnAttackStart();
        _anim.SetBool("Idle", false);
        _anim.SetBool("Walking", false);
        _anim.SetBool("Attacking", true);
        //GameManager.Instance.VolumeManager.ChangeVignette("#ff0000",new ClampedFloatParameter(1f,0f,1f));
        _target.ChangeFrameColor("#ff0000", 0.5f);
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
        _target.cameraShake = true;
        _target.underAttack = true;
        _target.attacker = this;
        _audioSource.clip = _clipAspiradora;
        _audioSource.Play();
        _waitKill = Time.time;
    }

    void Attack()
    {
        //if (!_gbFov.hasLOS)
        //{
        //    EndAttack(false); 
        //    return;
        //}
        if(_target.scapeSpam >= _spamScape)
        {
            EndAttack();
            _target.CrazyScape((_target.transform.position - transform.position).normalized);
            return;
        }
        Vector3 direction = _target.transform.position - transform.position;
        transform.Rotate(direction);
        _target.ApplyForce(-direction, _suctionForce);
        if (Vector3.SqrMagnitude(direction) <= (_killRange * _killRange) && Time.time - _waitKill > _atkDelay)
        {
            _target.GetDamage();
            _target.ResetMiss();
            _sourceDamage.Play();
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

    void EndAttack(bool setLastAttack = true)
    {
        //if(!_isAttacking) return;
        //Debug.Log("Terminando Ataque");
        //_agent.speed = speedNormal;
        if (OnAttackEnd != null)
            OnAttackEnd();
        _anim.SetFloat("zAxis", 0);
        _anim.SetBool("Attacking", false);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Walking", false);
        _tornadoGen.Stop();
        _target.ResetFrameColor();
        _target.underAttack = false;
        _target.attacker = null;
        _target.ApplyForce(new Vector3(), 0);
        _canAttack = false;
        _isAttacking = false;
        _angry = false;
        _target.cameraShake = false;

        if (setLastAttack)
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
            _waitTrampaRandom = Random.Range(minTrapTime, 121);
            var newTrap = Instantiate(trampaPrefab, lugar.position, Quaternion.identity);
            newTrap.Initialize(this);
        }

    }

    private IEnumerator ChaseTarget(float newWait)
    {
        
        _activeChase = true;
        //Debug.Log("<color=#825aef>Inicia Cazeria</color>");
        //consigue pos de Gus cada medio segundo
        
        //https://www.youtube.com/watch?v=5T5BY1j2MkE no abrir

        //if(!_angry) yield return null;
        WaitForSeconds wait = new WaitForSeconds(newWait);

        while (_angry && _canAttack && _agent.enabled)
        {
            //Debug.Log("<color=red>Cazando</color>");  
            _actualNode = _target.transform;
            _agent.SetDestination(_actualNode.position  );
            yield return wait;
        }

        if (!_isAttacking && _canAttack && _agent.enabled)
        {
            //Quiero creer que esto es para dejar de perseguir;

            //Debug.Log("<color=#ef5ae4>Termina Cazeria</color>");
            //_actualNode = GetNewNode(_actualNode);
            //_agent.SetDestination(_actualNode.position);

            SetNewDestination(_actualNode);
            yield return null;
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
        //if (_gbFov.hasLOS)
        if(!inRagdoll)
            StartAttack();

        //yield return new WaitForSeconds(_attackDuration);
        
        //EndAttack();
    }

    //protected override Transform GetNewNode(Transform lastNode = null)
    //{
    //    if (!_canAttack) return null;            
    //    return base.GetNewNode(lastNode);
    //}

    public virtual void AttackShadow(GameObject shadow)
    {
        if (!_canAttack) return;
        if (_angry)
        {
            StopAnger();
        }
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

    protected override void OnDestroy()
    {
        GameManager.Instance.Gb.Remove(this);
        base.OnDestroy();    
    }

    float lastSlide = -1, minSlideTime = 0.1f;

    public virtual void StartSlide(Vector3 dir ,float _impulseForce = 8f)
    {
        if (_isAttacking) return;
        //Debug.Log("<color=green> Slide de Asustable </color>");
        //desactivar navmesh
        //activar gravedad
        //desactivar friccion
        //dar impulso
        //setear animacion

        //En vez de que salga disparado el GB podemos hacer que se caiga

        StopAnger();
        //var dir = transform.forward;
        //var _impulseForce = 8f;

        _agent.enabled = false;
        _sliding = true;
        _rb.useGravity = true;
        _rb.drag = 0f;
        _rb.AddForce(dir * _impulseForce * _rb.mass, ForceMode.Impulse);

    }

    public virtual void StopSlide()
    {
        //desactivar gravedad
        //activar friccion?
        //activar navmesh
        //sacar animacion
        _sliding = false;
        _rb.useGravity = false;
        _rb.drag = 1f;
        _rb.velocity = Vector3.zero;
        _agent.enabled = true;

        //GetAngry();
        //Spawn scaner pulse
        GetScared(1,actualRoom);

        //_agent.SetDestination(_actualNode.position);

        OnSlideStop();

    }

    public override void TurnOff()
    {
        transform.GetComponent<Collider>().enabled = false;
        //_AIActive = false;
        _agent.speed = 0f;
        _agent.enabled = false;
        _mesh.SetActive(false);
    }

    public override void TurnOn()
    {
        transform.GetComponent<Collider>().enabled = true;
        _mesh.SetActive(true);
        //_AIActive = true;
        _agent.enabled = true;
        _agent.speed = speedNormal;
        if (_actualNode != null)
            _agent.SetDestination(_actualNode.position);
    }

    //RAGDOLL HERE
    #region Raggdoll Calls
    public virtual void CallRagdollOn()
    {
        if (!canRagdoll) return;
        if (_angry) StopAnger();
        if (_isAttacking) EndAttack();
        if (_fighting) StartCoroutine(StopFight(0));
        if (_sliding) StopSlide();
        if (_startingAttack) _startingAttack = false;
        //StopAnger();
        //EndAttack();
        _myRagdollSwitch.ActivateRagdoll();
        OnRagdollTrigger();
        inRagdoll = true;
    }


    public virtual void CallRagdollOn(Vector3 dir)
    {
        //if (!canRagdoll) return;
        //if (_angry) StopAnger();
        //if (_isAttacking) EndAttack();
        //if (_fighting) StartCoroutine(StopFight(0));
        //if (_sliding) StopSlide();
        //if (_startingAttack) _startingAttack = false;

        //_myRagdollSwitch.ActivateRagdoll(dir);
        //OnRagdollTrigger();
        //inRagdoll = true;

        if (!canRagdoll) return;
        if (_angry) StopAnger();
        if (_isAttacking) EndAttack();
        if (_fighting) StartCoroutine(StopFight(0));
        if (_sliding) StopSlide();
        if (_startingAttack) _startingAttack = false;
        //StopAnger();
        //EndAttack();
        _myRagdollSwitch.ActivateRagdoll(dir);
        OnRagdollTrigger();
        inRagdoll = true;

    }
    public virtual void CallRagdollOff(float wait = 0f, bool scareOnEnd = false)
    {
        StartCoroutine(RagdollOff(wait, scareOnEnd));
    }

    private IEnumerator RagdollOff(float wait = 0f, bool scareOnEnd = false)
    {
        yield return new WaitForSeconds(wait);
        _myRagdollSwitch.DeactivateRagdoll();
        if (scareOnEnd)
        {
            //_canAttack = true;
            GetScared(1f, actualRoom,_actualNode);
        }
        if (_actualNode == GameManager.Instance.Player)
        {
            //_actualNode = GetNewNode();
            //_agent.SetDestination(_actualNode.position);

            SetNewDestination();
        }
        inRagdoll = false;

        //SetSpeed();



        //if (tutorial == true)
        //{
        //    Destroy(gameObject);
        //}
    } 
    #endregion



    //REWORK STARTS HERE

    public void AddToRepairList(GB_Gadget gadget)
    {
        //Debug.Log($"<color=magenta> Objeto agregado </color>");


        _brokenGadgets.Add(gadget);

        _hasObjToRepair = true;

    }

    void GoRepairGadget()
    {
        //muro de if, si no esta haciendo nada, intenta reparar
        /*
         * no atacando
         * no enojado
         * no dudando?
         * no resbalando
         * no ragdoll
        */

        //ir a pos de objeto roto index 0
        //reparar

        //Debug.Log($"<color=magenta> LLendo a reparar</color>");


        if (_isAttacking) return;
        if (_angry) return;
        if (_doubt) return;
        if (_sliding) return;
        if (inRagdoll) return;

        //Debug.Log($"<color=magenta> Pase el muro de if </color>");


        _isTringToRepair = true;

        _agent.SetDestination(_brokenGadgets[0].transform.position);
    }

    IEnumerator StartRepairGadget(float wait)
    {
        //do anim de reparar
        //Debug.Log($"<color=magenta> Intentando Reparar </color>");


        yield return new WaitForSeconds(wait);

        //Debug.Log($"<color=magenta> Reparar llamado </color>");


        if (_inRepairPos)
            RepairGadgets(_brokenGadgets[0]);
    }

    void RepairGadgets(GB_Gadget gadget)
    {
        //Reparar objeto
        //Dejar de intentar reparar
        //Checkear si quedan por arreglar
        Debug.Log($"<color=magenta> Objeto reparado </color>");

        gadget.Repair();
        _brokenGadgets.Remove(gadget);

        _isTringToRepair = false;

        if (_brokenGadgets.Count < 1)
            _hasObjToRepair = false;

        SetNewDestination(null);
    }

    private IEnumerator LookAround()
    {
        _lookingActive = true;

        _anim.SetBool("Walking", false);
        _anim.SetBool("Idle", false);
        //_anim.SetBool("Search", false);//no esta en anim
        //_anim.SetBool("Doubt", false);//tampoco
        //_anim.SetBool("InPos", true);//vos sabes


        var _waitRandom = Random.Range(2f, 5f);

        WaitForSeconds wait = new WaitForSeconds(_waitRandom);
        yield return wait;

        _anim.SetBool("Walking", true);
        //_anim.SetBool("InPos", false);//no existe en anim
        _anim.SetBool("Idle", false);
        //_anim.SetBool("Search", false);//same

        //_actualNode = GetNewNode(_actualNode);
        //_agent.SetDestination(_actualNode.position);
        SetNewDestination(_actualNode);

        SetSpeed();

        _lookingActive = false;
    }

    public override void SetNewDestination(Transform lastDest = null)
    {

        //if (lastDest != null)
        //    _actualNode = GetNewNode(lastDest);
        //else
        //    _actualNode = GetNewNode();

        //_agent.SetDestination(_actualNode.position);
        base.SetNewDestination(lastDest);

        SetSpeed();
        //Debug.Log($"<color=cyan> Nuevo Destino Elegido {_actualNode.name} </color>");
    }

}
