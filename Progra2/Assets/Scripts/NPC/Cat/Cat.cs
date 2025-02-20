using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody  ))]
public class Cat : NPC
{
    [Header("<color=#560833> Lucifer, Ruler of mankind </color>")]
    
    [SerializeField] Pickable _targetObject;
    [SerializeField] float _jumpCD, _jumpDis, _jumpForce, _dropDis;
    [SerializeField] bool _canJump, _onFloor, _searchObj;
    [SerializeField] LayerMask _mask, _floorMask;
    [SerializeField] List<AudioClip> _clips;//why not an array? ni...
    Rigidbody _rb;
    float _lastJump, _rbDrag;
    bool _antiSpam;

    //Rework
    DelegateType.VoidDelegate CountDown = delegate { };
    [Header("<color=#00ffff> JumpShit </color>")]
    [SerializeField] CatAlertIcon _alertIcon;
    [SerializeField] float _fightDuration;
    [SerializeField] public float timeToJump;
    [SerializeField] Transform _triggerHolder;
    [SerializeField] CatArea _triggerPrefab;
    [SerializeField] float _chaseDuration;
    [SerializeField, Tooltip("firs Min Time, sec Max Time")] float[] _timeBtChase = new float[2];

    CatArea _myGusDetector;
    float _fightTime = 0f;
    bool _canJumpToPlayer = true;
    

    protected override void Start()
    {
        base.Start();
        //yield return null;
        //StartCoroutine(CheckForObjects());
        _rb = GetComponent<Rigidbody>();
        _rbDrag = _rb.drag;

        //SpawnCatTrigger
        _alertIcon = GetComponentInChildren<CatAlertIcon>();
        _alertIcon.SetMaxTimers(timeToJump, 1);


        _myGusDetector = Instantiate(_triggerPrefab, _triggerHolder.position, Quaternion.identity);
        _myGusDetector.Initialize(this, _triggerHolder);

        StartCoroutine(StopChase());
    }

    private void Update()
    {
        if (!_AIActive) return; 
        //if (_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();

        if (_agent.enabled   && (!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            //Debug.Log("<color=#26c5f0> LLege al destino </color>");

            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);
        }

        //if (_doubt)
        //    _searchingTimer += Time.deltaTime;
        //if (_searchingTimer > 12f) StopSearching();
        //if (_doubt && _inPlace && _waitDoubt >= 2)
        //{
        //    StopSearching();
        //}


        if (!_canJump && Time.time - _lastJump > _jumpCD)
        {
            _canJump = true;
            _searchObj = true;
        }

        if (_targetObject && _canJump)
        {
            //_agent.isStopped = true;
            //Imposible de despegar al gato del piso
            JumpToObject();
        }

        RaycastHit _floor;

        if(!_onFloor && Time.time - _lastJump > _jumpCD / 2)
        {
            OnFloor();
        }
        else if (!_onFloor && Time.time - _lastJump > 0.05f)
        {
            if (Physics.Raycast(transform.position, -transform.up, out _floor, 0.3f, LayerMask.GetMask("NoTras")))
            {
                OnFloor();
            }
        }


        if (!_antiSpam && _searchObj)
        {
            StartCoroutine(CheckForObjects());
        }
        //else
        //    Debug.Log($"<color=blue> AntiSpam = {_antiSpam} || SerchObj = {_searchObj}</color>");

        if(!_onFloor && _targetObject != null && Vector3.SqrMagnitude(transform.position - _targetObject.transform.position) <= (_dropDis * _dropDis))
        {
            _targetObject.Drop();
        }

        CountDown();
        DoJumpCountDown();
    }

    void CheckObjects()
    {
        //if (true) return;// lo se, soy un capo para desactivar cosas

        _targetObject = null;
        Collider[] _objs;
        //Debug.Log("Chequeando");
        _objs = Physics.OverlapSphere(transform.position, _jumpDis, _mask);
        foreach (Collider obj in _objs)
        {
            //Debug.Log($"<color=orange>Detectado {obj.name}</color>");
            if (obj.TryGetComponent<Pickable>(out Pickable p) && p.holding == true)
            {
                //Debug.Log($"<color=orange>Detectado {p.name}</color>");
                //Debug.Log("Encontrado");
                _targetObject = p;
                Debug.Log($"<color=magenta>{p.name} Agarrado</color>");

                //Debug.Log($"<color=green>Target {_targetObject.name}</color>");
            }
        }
    }

    private IEnumerator CheckForObjects()
    {
        _antiSpam = true;
        
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        Debug.Log($"<color=green>CheckForObject Activo</color>");
        while (_searchObj)
        {
            yield return wait;
            #region comment
            //_targetObject = null;
            //Collider[] _objs;
            //Debug.Log("Chequeando");
            //_objs = Physics.OverlapSphere(transform.position, _jumpDis, _mask);
            //foreach (Collider obj in _objs)
            //{
            //    //Debug.Log($"<color=orange>Detectado {obj.name}</color>");
            //    if (obj.TryGetComponent<Pickable>(out Pickable p) && p.holding == true)
            //    {
            //        //Debug.Log($"<color=orange>Detectado {p.name}</color>");
            //        Debug.Log("Encontrado");
            //        _targetObject = p;
            //        Debug.Log($"<color=green>Target {_targetObject.name}</color>");
            //    }
            //} 
            #endregion

            CheckObjects();
            
        }

        Debug.Log($"<color=red>CheckForObject Desactivado</color>");

        //Debug.Log("Stoped");
    }

    void JumpToObject()
    {
        if (true) return;// lo se, soy un capo para desactivar cosas

        var dir = (_targetObject.transform.position - transform.position).normalized;
        _agent.enabled = false;
        _canJump = false;
        _antiSpam=false;
        _rb.useGravity = true;
        _onFloor = false;
        _searchObj = false;
        //_rb.AddForce(transform.up * _jumpForce  , ForceMode.Impulse);
        _rb.drag = 0f;
        transform.forward = new Vector3(dir.x, 0, dir.z);
        _rb.AddForce(transform.up * _jumpForce* _rb.mass * 0.5f, ForceMode.Impulse);
        _rb.AddForce(dir * _jumpForce * _rb.mass, ForceMode.Impulse);

        //_targetObject.Drop();

        SelectAudio();

        _targetObject = null;
        _lastJump = Time.time;
    }

    void OnFloor()
    {
        _audioSource.clip = null;
        if (_onFloor) return;
        //StartCoroutine(CheckForObjects());
        _targetObject = null;
        _rb.velocity = Vector3.zero;
        _rb.drag = _rbDrag;
        _agent.enabled = true;
        _agent.SetDestination(_actualNode.position);
        _rb.useGravity = false;
        _onFloor = true;

        //_searchObj = true;
    }

    public override void GetDoubt(Vector3 pos, int g)
    {
        //base.GetDoubt(pos);
    }

    private void SelectAudio()
    {
        int random = Random.Range(1, _clips.Count);
        _audioSource.clip = _clips[random]; 
        _audioSource.Play();
    }

    //Rework

    bool _gusInRange;
    float _jumpPlayerTimer = 0;
    DelegateType.VoidDelegate DoJumpCountDown = delegate { };

    public void StartAlert()
    {
        if (!_canJumpToPlayer) return;

        _agent.speed = 0;

        //_alertIcon.active = true;
        _gusInRange = true;
        //Start CountDoun
        DoJumpCountDown = JumpPlayerCountDown;
        _alertIcon.active = true;
        _searchObj = true;

        StartCoroutine(StopChase());
    }

    public void StopAlert() 
    {
        
        Debug.Log($"<color=magenta>Chilling</color>");

        _searchObj = false;
        DoJumpCountDown = delegate { };

        _agent.speed = speedNormal;

        _gusInRange = false;
        _jumpPlayerTimer = 0;

        _alertIcon.active = false;
    }

    void JumpPlayerCountDown()
    {
        _jumpPlayerTimer += Time.deltaTime;
        if (_jumpPlayerTimer % 1 == 0)
            Debug.Log($"<color=red>Contando para saltarle al pedazo de puto de Gus</color>");

        if(_gusInRange && _jumpPlayerTimer > timeToJump)
        {
            DoJumpCountDown = delegate { };
            _jumpPlayerTimer = 0;
            _gusInRange = false;
            JumpToPLayer();
        }

        _alertIcon.timer = _jumpPlayerTimer;
        //_alertIcon.charge = _timeToBark;
    }



    public void JumpToPLayer()
    {
        if(!_canJumpToPlayer) return;
        Debug.Log($"<color=red>Andatehhhhhhhhhhhhhhh</color>");
        _alertIcon.active = false;

        Player player = GameManager.Instance.Player;
        var dir = ((player.transform.position+new Vector3(0,2,0)) - transform.position).normalized;
        _agent.enabled = false;
        _canJumpToPlayer = false;
        _antiSpam = false;
        _rb.useGravity = true;
        _onFloor = false;
        _searchObj = false;
        //_rb.AddForce(transform.up * _jumpForce  , ForceMode.Impulse);
        _rb.drag = 0f;
        transform.forward = new Vector3(dir.x, 0, dir.z);
        _rb.AddForce(transform.up * _jumpForce * _rb.mass * 0.5f, ForceMode.Impulse);
        _rb.AddForce(dir * _jumpForce * _rb.mass, ForceMode.Impulse);

        //_targetObject.Drop();

        SelectAudio();

        _lastJump = Time.time;

        player.InvertMovement();

        CountDown = LeaveCountDown;
        if (_targetObject != null)
        {
            _targetObject.Drop();
            _targetObject = null;
        }

    }

    void LeaveCountDown()
    {
        _fightTime += Time.deltaTime;

        if(_fightTime > _fightDuration) 
        {
            LeavePlayer();
        }
    }

    void LeavePlayer()
    {
        CountDown = delegate { };
        _fightTime = 0;
        _canJumpToPlayer = true;

        GameManager.Instance.Player.RestoreNormalMovement();

    }

    private void OnEnable()
    {
        if(_myGusDetector != null)
            _myGusDetector.active = true;
    }

    private void OnDisable()
    {
        if (_myGusDetector != null)
            _myGusDetector.active = false;
    }

    void StartChasePlayer()
    {
        StartCoroutine(ChaseTarget());
        StartCoroutine(ChaseDuration());
    }

    bool _activeChase = false;

    IEnumerator ChaseTarget()
    {
        _activeChase = true;
        var target = GameManager.Instance.Player;
        //Debug.Log("<color=#825aef>Inicia Cazeria</color>");
        //consigue pos de Gus cada medio segundo

        while (_activeChase && _onFloor && _canJump && _agent.enabled)
        {
            _actualNode = target.transform;
            _agent.SetDestination(_actualNode.position);

            yield return new WaitForSeconds(0.5f);
        }

        _activeChase = false;
        //yield return null;       
    }

    IEnumerator StopChase()
    {
        _activeChase = false;

        var nextChase = Random.Range(_timeBtChase[0], _timeBtChase[1]);

        _actualNode = GetNewNode(_actualNode);
        _agent.SetDestination(_actualNode.position);

        yield return new WaitForSeconds(nextChase);
        StartChasePlayer();
    }

    IEnumerator ChaseDuration()
    {

        yield return new WaitForSeconds(_chaseDuration);

        if( _activeChase )
            StartCoroutine(StopChase());

    }

}
