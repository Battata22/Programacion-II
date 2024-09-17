using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asustable : NPC
{
    //[Header("AI")]
    //[SerializeField] float _changeNodeDist = 0.5f;

    public float tiempoDeSusto, cdDeSusto;
    float _waitShivers, _waitscared, _waitRandom;//, _waitDoubt, _searchingTimer;

    public bool shivers = false, _scared = false;//, _doubt = false, _inPlace = false;
    bool _lookingActive =  false;

    //[SerializeField] float speedNormal, speedScared, speedDoubt;

    //AudioSource _audioSource;
    [SerializeField] AudioClip gritoClip, doubtClip;

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

    public override void GetScare()
    {               
        //Debug.Log("Susto de Asustable");
        _doubt = false;
        _particulas.scared = true;
        _audioSource.clip = gritoClip;
        _audioSource.Play();
        _waitscared = 0;
        GetNewNode(_actualNode);
        _scared = true;
        _agent.speed = speedScared;
        _agent.SetDestination(_actualNode.position);
    }

    protected override void StopScare()
    { 
        _agent.speed = speedNormal;
        _scared = false;
        _particulas.scared = false;
    }


    public override void GetShivers()
    {
        if (shivers) return;
        //Debug.Log("Escalofios de asustable");
        _audioSource.Play();
        _waitShivers = 0;
        _agent.speed = 0;
        shivers = true;
    }

    protected override void StopShivers()
    {
        _agent.speed = speedNormal;
    }

    public override void GetDoubt(Vector3 pos)
    {
        if (_scared) return;
        //Debug.Log("Duda de asustable");

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
            //Debug.Log($"<color=#adf947> LLegue espero por {_waitRandom} segundos </color>");
        }
        else
            _waitRandom = 0f;

        yield return new WaitForSeconds(2f);

        _actualNode = GetNewNode(_actualNode);

        _agent.SetDestination(_actualNode.position);

        _lookingActive = false;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Npc.Remove(this);
    }
}
