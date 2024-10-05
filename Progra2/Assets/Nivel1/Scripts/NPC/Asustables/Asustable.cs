using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Asustable : NPC
{
    //[Header("AI")]
    //[SerializeField] float _changeNodeDist = 0.5f;

    public float tiempoDeSusto, cdDeSusto;
    float _waitShivers, _waitscared, _waitRandom;//, _waitDoubt, _searchingTimer;

    public bool shivers = false, _scared = false;//, _doubt = false, _inPlace = false;
    bool _lookingActive =  false;

    [SerializeField] Slider _sliderBarra;

    [SerializeField] AudioClip gritoClip, doubtClip;
    [SerializeField] Animator _anim;

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
    protected override void Start()
    {
        base.Start();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!_AIActive) return;
        if (_actualNode == null) Initialize();
        if ((!_doubt && !_lookingActive &&Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            

            StartCoroutine(LookAround());
            //_actualNode = GetNewNode(_actualNode);

            //_agent.SetDestination(_actualNode.position);

            //Debug.Log($"Nodo actua {_actualNode}");
        }
        if (_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist))
        {
            _agent.speed = 0;

            if (!_inPlace)
            {
                //_inPlace = true;
                //_waitDoubt = 0;

                StartSearching();
            }
        }


        _waitShivers += Time.deltaTime;
        _waitscared += Time.deltaTime;
        if (_doubt)
            _searchingTimer += Time.deltaTime;

        if (_searchingTimer > 12f) StopSearching();

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

        if (_scared == true && _waitscared >= 5)
        {
            //_agent.speed = speedNormal;
            //_scared = false;

            StopScare();
        }

        if (_doubt && _inPlace && _waitDoubt >= 2)
        {
            //_agent.speed = speedNormal;
            //_doubt = false;
            //_inPlace = false;
            //GetNewNode();
            //_agent.SetDestination(_actualNode.position);

            StopSearching();
        }

        //if (!_doubt) _waitDoubt = 0;


    }

    public override void GetScared()
    {
        if (_scared) return;
        //Debug.Log("Susto de Asustable");
        _anim.SetFloat("zAxis", 1f);
        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
        _doubt = false;
        _particulas.scared = true;
        _audioSource.clip = gritoClip;
        _audioSource.Play();
        _waitscared = 0;
        GetNewNode(_actualNode);
        _scared = true;
        _agent.speed = speedScared;
        _agent.SetDestination(_actualNode.position);
        Ganarga();
    }

    protected override void StopScare()
    {
        _anim.SetFloat("zAxis", 0f);
        
        _agent.speed = speedNormal;
        _scared = false;
        _particulas.scared = false;
    }


    public override void GetShivers(AudioClip a)
    {
        if (shivers) return;
        //Debug.Log("Escalofios de asustable");
        //a_audioSource.Play();
        _anim.SetBool("Walking", false);
        _anim.SetBool("Idle", true);
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
        _agent.speed = speedNormal;
    }

    public override void GetDoubt(Vector3 pos)
    {
        if (_scared) return;
        //Debug.Log("Duda de asustable");
        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
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

        if (!_scared)
        {
            _waitRandom = Random.Range(2f, 5f);
            //_anim.SetFloat("zAxis", 1f);
            _anim.SetBool("Walking", false);
            _anim.SetBool("Idle", true);
            //Debug.Log($"<color=#adf947> LLegue espero por {_waitRandom} segundos </color>");
        }
        else
            _waitRandom = 0f;
        WaitForSeconds wait = new WaitForSeconds(_waitRandom);
        if (_scared) wait = new WaitForSeconds(0f);
        yield return wait;

        _anim.SetBool("Walking", true);
        _anim.SetBool("Idle", false);
        _actualNode = GetNewNode(_actualNode);

        _agent.SetDestination(_actualNode.position);

        _lookingActive = false;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Npc.Remove(this);
    }

    void Ganarga()
    {
        _sliderBarra.value++;
        if (_sliderBarra.value <= 1)
        {
            GameManager.Instance.Player._nivel = 1;
        }
        else if (_sliderBarra.value >= _sliderBarra.maxValue * 0.4 && GameManager.Instance.Player._nivel < 2)
        {
            GameManager.Instance.Player.LevelUp();
            GameManager.Instance.Master1.ActivarGB();
        }
        else if (_sliderBarra.value >= _sliderBarra.maxValue * 0.7 && GameManager.Instance.Player._nivel < 3)
        {
            GameManager.Instance.Player.LevelUp();
        }

        if (_sliderBarra.value >= _sliderBarra.maxValue)
        {
            SceneManager.LoadScene("Victoria");
        }
    }
}
