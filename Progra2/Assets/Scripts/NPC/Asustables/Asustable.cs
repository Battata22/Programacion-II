using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Asustable : NPC, ICanSlide, IPossessable
{
    //[Header("AI")]
    //[SerializeField] float _changeNodeDist = 0.5f;

    public float tiempoDeSusto, cdDeSusto, tiempoDeMoco, tiempoDeStun;
    public float _waitShivers, _waitscared, _waitRandom, waitMoco, waitStun;//, _waitDoubt, _searchingTimer;


    public bool shivers = false, scared = false, mocod = false, stuned = false;//, _doubt = false, _inPlace = false;
    bool _lookingActive = false;

    [SerializeField] Slider _sliderBarra;

    [SerializeField] AudioClip gritoClip, doubtClip;
    [SerializeField] Animator _anim;

    [SerializeField] GameObject _mesh;

    Rigidbody _rb;
    bool _sliding;

    EnableRagdoll _myRagdollSwitch;

    [SerializeField] bool tutorial = false;

    //Eventos
    public event DelegateType.VoidDelegate OnSlideStop = delegate { };
    public event DelegateType.VoidDelegate OnRagdollTrigger = delegate { };


    #region Comment
    //[SerializeField] float speedNormal, speedScared, speedDoubt;

    //AudioSource _audioSource;

    //[SerializeField] Transform _actualNode;
    //[SerializeField] List<Transform> _navMeshNodes = new();

    //Vector3 _searchingPos;
    //[SerializeField] bool _AIActive;

    //public List<Transform> NavMeshNodes
    //{
    //    get { return _navMeshNodes; }
    //    set { _navMeshNodes = value; }
    //}

    //NavMeshAgent _agent;

    //void Start()
    //{
    //    GameManager.Instance.Npc.Add(this);
    //    _audioSource = GetComponentInChildren<AudioSource>();
    //    _agent = GetComponent<NavMeshAgent>();
    //    _agent.speed = speedNormal;
    //    //Initialize();
    //    //_actualNode = GetNewNode();
    //    //No abrir https://www.youtube.com/watch?v=dQw4w9WgXcQ
    //}

    //public void Initialize()
    //{
    //    //_target = GameManager.Instance.Player.transform;

    //    _actualNode = GetNewNode();

    //    _agent.SetDestination(_actualNode.position);

    //    _AIActive = true;
    //}
    #endregion

    private void Awake()
    {
        _myRagdollSwitch = GetComponent<EnableRagdoll>();
    }

    protected override void Start()
    {
        if (tutorial == false)
        {
            base.Start();
            _rb = GetComponent<Rigidbody>();
            //yield return null;
            _anim = GetComponentInChildren<Animator>();

            PhaseManager.TrapPhaseActive += TrapPhase;
            PhaseManager.GameplayPhaseActive += GameplayPhase;
            gameObject.SetActive(false);
        }
        else if (tutorial == true)
        {
            base.Start();
            _rb = GetComponent<Rigidbody>();
            //yield return null;
            _anim = GetComponentInChildren<Animator>();

            PhaseManager.TrapPhaseActive += TrapPhase;
            PhaseManager.GameplayPhaseActive += GameplayPhase;

            GetScaredTuto();
            Destroy(gameObject, 6);
        }
    }

    private void Update()
    {
        //if (tutorial == true)
        //{
        //    GetScaredTuto();
        //}

        if (waitStun >= tiempoDeStun && stuned)
        {
            StopStun();
            stuned = false;
        }

        if (!_AIActive) return;
        if (_actualNode == null) Initialize();
        if ((!_doubt && !_lookingActive && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            StartCoroutine(LookAround());
            #region comment
            //_actualNode = GetNewNode(_actualNode);

            //_agent.SetDestination(_actualNode.position);

            //Debug.Log($"Nodo actua {_actualNode}"); 
            #endregion
        }
        if (_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist))
        {
            _agent.speed = 0;

            if (!_inPlace)
            {
                //_inPlace = true;
                //_waitDoubt = 0;

                _anim.SetBool("Doubt", false);
                StartSearching();
                _anim.SetBool("Search", true);
            }
        }


        _waitShivers += Time.deltaTime;
        _waitscared += Time.deltaTime;
        waitMoco += Time.deltaTime;
        waitStun += Time.deltaTime;
        if (_doubt)
            _searchingTimer += Time.deltaTime;

        if (_searchingTimer > 12f)
        {
            StopSearching();
            _anim.SetBool("Walking", true);
            _anim.SetBool("Doubt", false);
        }

        if (_inPlace) _waitDoubt += Time.deltaTime;

        if (_waitShivers >= tiempoDeSusto && shivers == true)
        {
            //_agent.speed = speedNormal;
            StopShivers();
        }
        if (_waitShivers >= cdDeSusto && shivers == true)
        {
            shivers = false;
        }

        if (waitMoco >= tiempoDeMoco && mocod)
        {
            StopMoco();
            mocod = false;
        }



        if (scared == true && _waitscared >= tiempoDeSusto)
        {
            //_agent.speed = speedNormal;
            //_scared = false;

            StopScare();
        }

        if (_doubt && _inPlace && _waitDoubt >= 2)
        {
            #region comment
            //_agent.speed = speedNormal;
            //_doubt = false;
            //_inPlace = false;
            //GetNewNode();
            //_agent.SetDestination(_actualNode.position);
            #endregion

            StopSearching();
            _anim.SetBool("Walking", true);
            _anim.SetBool("Search", false);
        }

        //if (!_doubt) _waitDoubt = 0;

        if (Input.GetKeyDown(KeyCode.M))
        {
            Ganarga(4f);
            //Ganarga(1f);
            //Ganarga(1f);
            //Ganarga(1f);
        }

        if (_sliding && Time.time - lastSlide > minSlideTime && _rb.velocity.sqrMagnitude < 0.1f * 0.1f)
        {
            StopSlide();
        }

    }

    public override void GetScared(float scareAmount, Transform direction = null)
    {
        if (!_AIActive) return;
        //if (_scared) return;
        //if (scareAmount < 0.1f) return;
        //Debug.Log("Susto de Asustable");
        _anim.SetFloat("zAxis", 1f);
        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);
        _anim.SetBool("Doubt", false);

        _doubt = false;
        _particulas.scared = true;
        scared = true;
        _agent.speed = speedScared;
        _waitRandom = 0f;
        _audioSource.clip = gritoClip;
        _audioSource.Play();
        _waitscared = 0;
        GetNewNode(_actualNode);
        if (direction != null)
            _actualNode = direction;
        _agent.SetDestination(_actualNode.position);
        Ganarga(scareAmount);

    }

    protected override void StopScare()
    {
        //if (!_AIActive) return;
        _anim.SetFloat("zAxis", 0f);
        _anim.SetBool("Doubt", false);

        _agent.speed = speedNormal;
        scared = false;
        _particulas.scared = false;
    }


    public override void GetShivers(AudioClip a)
    {
        if (!_AIActive) return;
        if (shivers) return;
        //Debug.Log("Escalofios de asustable");
        //a_audioSource.Play();
        _anim.SetBool("Walking", false);
        _anim.SetBool("Idle", true);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);
        _anim.SetBool("Doubt", false);

        _audioSource.clip = a;
        _audioSource.Play();
        _waitShivers = 0;
        _agent.speed = 0;
        shivers = true;
    }

    protected override void StopShivers()
    {
        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);

        _agent.speed = speedNormal;
    }

    public void GetMoco(AudioClip a)
    {

        _anim.SetBool("Walking", false);
        _anim.SetBool("Idle", true);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);
        _anim.SetBool("Doubt", false);

        _audioSource.clip = a;
        _audioSource.Play();
        _agent.speed = 0;
        waitMoco = 0;
        mocod = true;
    }

    public void StopMoco()
    {
        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);

        _agent.speed = speedNormal;
    }

    public void StartSlow()
    {
        //_anim.SetBool("Walking", false);
        //_anim.SetBool("Idle", false);
        //_anim.SetBool("InPos", false);
        //_anim.SetBool("Search", false);
        //_anim.SetBool("Doubt", false);

        _agent.speed = _agent.speed * 0.2f;
    }

    public void StopSlow()
    {
        //_anim.SetBool("Walking", false);
        //_anim.SetBool("Idle", false);
        //_anim.SetBool("InPos", false);
        //_anim.SetBool("Search", false);
        //_anim.SetBool("Doubt", false);

        _agent.speed = speedNormal;
    }

    public void GetStun(AudioClip a)
    {
        //if (!_AIActive) return;
        if (stuned) return;
        //if (!scared) return;
        _anim.SetBool("Walking", false);
        _anim.SetBool("Idle", true);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);
        _anim.SetBool("Doubt", false);

        _audioSource.clip = a;
        _audioSource.Play();
        _agent.speed = 0;
        waitStun = 0;
        stuned = true;

        //_myRagdollSwitch.ActivateRagdoll();
        CallRagdollOn();
    }

    public void StopStun()
    {
        print("<color=magenta> Asustable Stop Stun </color>");
        _myRagdollSwitch.DeactivateRagdoll();
        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);

        _agent.speed = speedNormal;
    }

    public override void GetDoubt(Vector3 pos)
    {
        if (!_AIActive) return;
        if (scared) return;
        //Debug.Log("Duda de asustable");
        _anim.SetBool("Doubt", true);
        _anim.SetBool("Walking", false);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Search", false);
        _doubt = true;

        _audioSource.clip = doubtClip;
        _audioSource.Play();

        _agent.speed = speedDoubt;

        _agent.SetDestination(pos);
        _searchingPos = pos;
    }

    private IEnumerator LookAround()
    {
        _lookingActive = true;

        if (!scared)
        {
            _waitRandom = Random.Range(2f, 5f);
            //_anim.SetFloat("zAxis", 1f);
            _anim.SetBool("Walking", false);
            _anim.SetBool("Idle", false);
            _anim.SetBool("Search", false);
            _anim.SetBool("Doubt", false);


            //Debug.Log($"<color=#adf947> LLegue espero por {_waitRandom} segundos </color>");
        }
        else
            _waitRandom = 0f;

        _anim.SetBool("InPos", true);

        WaitForSeconds wait = new WaitForSeconds(_waitRandom);
        if (scared) wait = new WaitForSeconds(0f);
        yield return wait;

        _anim.SetBool("Walking", true);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Search", false);

        if (!_doubt && !possesed)
        {
            _actualNode = GetNewNode(_actualNode);
            _agent.SetDestination(_actualNode.position);
        }

        _lookingActive = false;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Npc.Remove(this);
    }

    void Ganarga(float num)
    {
        _sliderBarra.value += num;

        GameManager.Instance.terrorBar.value += num;

        //if (_sliderBarra.value <= 1)
        //{
        //    GameManager.Instance.Player.nivel = 1;
        //}
        //else if (_sliderBarra.value >= _sliderBarra.maxValue * 0.4 && GameManager.Instance.Player.nivel < 2)
        //{
        //    GameManager.Instance.Player.LevelUp();
        //    GameManager.Instance.Master1.ActivarGB();
        //}
        //else if (_sliderBarra.value >= _sliderBarra.maxValue * 0.7 && GameManager.Instance.Player.nivel < 3)
        //{
        //    GameManager.Instance.Player.LevelUp();
        //}

        //if (_sliderBarra.value >= _sliderBarra.maxValue)
        //{
        //    SceneManager.LoadScene("Victoria");
        //}
    }

    float lastSlide = -1, minSlideTime = 0.1f;
    
    public void StartSlide(Vector3 dir, float _impulseForce = 8f)
    {
        Debug.Log("<color=green> Slide de Asustable </color>");
        //desactivar navmesh
        //activar gravedad
        //desactivar friccion
        //dar impulso
        //setear animacion

        //var dir = transform.forward;
        //var _impulseForce = 8f;
        StopSearching();

        transform.forward = dir;

        _agent.enabled = false;
        _sliding = true;
        _rb.useGravity = true;
        _rb.drag = 0f;
        _rb.AddForce(dir * _impulseForce * _rb.mass, ForceMode.Impulse);

        lastSlide = Time.time;
    }

    public void StopSlide()
    {
        //desactivar gravedad
        //activar friccion?
        //activar navmesh
        //sacar animacion

        _sliding = false;
        _rb.useGravity = false;
        _rb.drag = 1f;
        _rb.velocity = Vector3.zero;

        //en vez de activar podemos llamar a Stun de golpe o caida despues de resbalar

        _agent.enabled = true;
        _agent.SetDestination(_actualNode.position);

        OnSlideStop();

    }

    bool possesed = false;

    public void GetPossess()
    {
        print("Llamado a poseer");
        if (possesed) return;
        _particulas.scared = false;
        StopScare();
        StopSearching();

        possesed = true;
        stuned = false;

        //Add PossessBehavior
        transform.AddComponent<PossessBehavior>();
        _agent.enabled = false;
        _rb.useGravity = true;
        this.enabled = false;
    }
    public void EndPossession()
    {
        print("Saliendo de posesion");
        if (!possesed) return;
        possesed = false;

        _rb.useGravity = false;
        _agent.enabled = true;
        _agent.SetDestination(_actualNode.position);
        //Remove PossessBehavior
    }

    void TrapPhase()
    {
        print($"<color=#8315d6> Asustable en fase de trampas </color>");
        _AIActive = false;
        _agent.speed = 0f;
        _agent.enabled = false;
    }

    void GameplayPhase()
    {
        //print($"<color=#15d629> Asustable en fase de Gameplay </color>");
        _AIActive = true;
        _agent.enabled = true;
        _agent.speed = speedNormal;
        if (_actualNode != null)
            _agent.SetDestination(_actualNode.position);
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

    public void CallRagdollOn()
    {
        _myRagdollSwitch.ActivateRagdoll();
        OnRagdollTrigger();
    }

    public void CallRagdollOn(Vector3 dir)
    {
        _myRagdollSwitch.ActivateRagdoll(dir);
        OnRagdollTrigger();
    }
    /// <summary>
    /// Cambia de ragdoll a NPC normal
    /// </summary>
    /// <param name="wait"> Delay al ser llamado (el objeto que llama debe existir por mas tiempo que el delay)</param>
    /// <param name="scareOnEnd"> Asustar NPC al activarse</param>
    /// <returns></returns>
    /// 

    public void CallRagdollOff(float wait = 0f, bool scareOnEnd = false)
    {
        StartCoroutine(RagdollOff(wait, scareOnEnd));
    }

    private IEnumerator RagdollOff(float wait = 0f , bool scareOnEnd = false)
    {
        yield return new WaitForSeconds(wait);
        _myRagdollSwitch.DeactivateRagdoll();
        if (scareOnEnd)
            GetScared(1f, _actualNode);
        if (tutorial == true)
        {
            Destroy(gameObject);
        }
    }

    public void GetScaredTuto()
    {
        _anim.SetFloat("zAxis", 1f);
        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Search", false);
        _anim.SetBool("Doubt", false);

        _doubt = false;
        //_particulas.scared = true;
        scared = true;
        _agent.speed = speedScared;
        _waitRandom = 0f;
        _audioSource.clip = gritoClip;
        _audioSource.Play();
        _waitscared = 0;

        
        _rb.AddForce(transform.forward * 50 * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
