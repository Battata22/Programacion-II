using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] float _changeNodeDist = 0.5f;

    public float tiempoDeSusto, cdDeSusto;
    float _wait, _waitscared, _waitDoubt, _maxTimeSearching;

    public bool shivers = false, _scared = false, _doubt = false, _inPlace = false;

    [SerializeField] float speedNormal, speedScared, speedDoubt;

    AudioSource _audioSource;
    [SerializeField] AudioClip gritoClip;

    [SerializeField] Transform _actualNode;
    [SerializeField] List<Transform> _navMeshNodes = new();

    Vector3 _searchingPos;

    public List<Transform> NavMeshNodes    
    { 
        get { return _navMeshNodes; }
        set { _navMeshNodes = value; }
    }

    NavMeshAgent _agent;

    void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        GameManager.Instance.Npc.Add(this);
        //_actualNode = GetNewNode();
    }

    public void Initialize()
    {
        //_target = GameManager.Instance.Player.transform;

        _actualNode = GetNewNode();

        _agent.SetDestination(_actualNode.position);
    }

    private void Update()
    {
        if((!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);

            //Debug.Log($"Nodo actua {_actualNode}");
        }
        if(_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist ))
        {
            _agent.speed = 0;

            if (!_inPlace) 
            { 
                //_inPlace = true;
                //_waitDoubt = 0;

                StartSearching();
            }
        }


        _wait += Time.deltaTime;
        _waitscared += Time.deltaTime;
        if(_doubt)
            _maxTimeSearching += Time.deltaTime;

        if(_maxTimeSearching > 12f)StopSearching();

        if(_inPlace)_waitDoubt += Time.deltaTime;

        if(_wait >= tiempoDeSusto && shivers == true)
        {
            //_agent.speed = speedNormal;
            StopShivers();
        }
        if(_wait >= cdDeSusto && shivers == true)
        {
            shivers = false;
        }

        if (_scared == true && _waitscared >= 5)
        {
            //_agent.speed = speedNormal;
            //_scared = false;

            StopScare();
        }

        if (_doubt && _inPlace &&_waitDoubt>=2)
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

    Transform GetNewNode(Transform lastNode = null)
    {
        Transform newNode = _navMeshNodes[Random.Range(1, _navMeshNodes.Count)];

        while(lastNode == newNode) 
        {
            newNode = _navMeshNodes[Random.Range(1, _navMeshNodes.Count)];
        }
        
        return newNode;
    }

    public void GetScare()
    {
        _doubt=false;
        _audioSource.clip = gritoClip;
        _audioSource.Play();
        _waitscared = 0;
        GetNewNode(_actualNode);
        _scared = true;
        _agent.speed = speedScared;
        _agent.SetDestination(_actualNode.position);
    }

    void StopScare()
    {
        _agent.speed = speedNormal;
        _scared = false;
    }


    public void GetShivers()
    {
       _wait = 0;
       _agent.speed = 0;
       shivers = true;
    }

    private void StopShivers()
    {
        _agent.speed = speedNormal;
    }

    public void GetDoubt(Vector3 pos)
    {
        if (_scared) return;
        print("estamos en eso");
        
        _doubt = true;
        Debug.Log("DUDOSO");
                
        _agent.speed = speedDoubt;
        
        _agent.SetDestination(pos);
        _searchingPos = pos;
    }

    void StartSearching()
    {
        _inPlace = true;
        _waitDoubt = 0;
    }

    void StopSearching()
    {
        _maxTimeSearching = 0;
        _agent.speed = speedNormal;
        _doubt = false;
        _inPlace = false;
        GetNewNode();
        _agent.SetDestination(_actualNode.position);
    }
}
