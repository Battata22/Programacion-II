using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(AudioSource))]

public abstract class NPC : MonoBehaviour
{
    [Header("<color=#e3f4aa>AI</color>")]
    [SerializeField] protected float _changeNodeDist = 0.5f;

    //public float tiempoDeSusto, cdDeSusto;
    //float _waitShivers, _waitscared, _waitDoubt, _maxTimeSearching;
    protected float _searchingTimer, _waitDoubt;

    public bool _inPlace = false, _doubt = false;
    //public bool shivers = false, _scared = false;

    [SerializeField]protected float speedNormal, speedScared, speedDoubt;

    public AudioSource _audioSource;
    //[SerializeField] AudioClip gritoClip, doubtClip;

    [SerializeField] protected Transform _actualNode;
    [SerializeField] protected List<Transform> _navMeshNodes = new();
    //protected Animator _anim;

    public List<Transform> NavMeshNodes    
    { 
        get { return _navMeshNodes; }
        set { _navMeshNodes = value; }
    }

    protected Vector3 _searchingPos;
    [SerializeField] protected bool _AIActive;

    protected NavMeshAgent _agent;

    protected Particulas _particulas;

    protected virtual void Start()
    {
        GameManager.Instance.Npc.Add(this);
        _audioSource = GetComponentInChildren<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = speedNormal;
        _particulas = GetComponentInChildren<Particulas>();
        //Initialize();
        //_actualNode = GetNewNode();
    }

    public void Initialize()
    {
        //_target = GameManager.Instance.Player.transform;

        _actualNode = GetNewNode();

        _agent.SetDestination(_actualNode.position);

        _AIActive = true;
    }

    #region Comment
    //private void Update()
    //{
    //    if (!_AIActive) return;
    //    if(_actualNode == null) Initialize();
    //    if((!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
    //    {
    //        _actualNode = GetNewNode(_actualNode);

    //        _agent.SetDestination(_actualNode.position);

    //        //Debug.Log($"Nodo actua {_actualNode}");
    //    }
    //    if(_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist ))
    //    {
    //        _agent.speed = 0;

    //        if (!_inPlace) 
    //        { 
    //            //_inPlace = true;
    //            //_waitDoubt = 0;

    //            StartSearching();
    //        }
    //    }


    //    //_waitShivers += Time.deltaTime;
    //    //_waitscared += Time.deltaTime;
    //    if(_doubt)
    //        _searchingTimer += Time.deltaTime;

    //    if(_searchingTimer > 12f)StopSearching();

    //    if(_inPlace)_waitDoubt += Time.deltaTime;

    //    //if(_waitShivers >= tiempoDeSusto && shivers == true)
    //    //{
    //    //    //_agent.speed = speedNormal;
    //    //    StopShivers();
    //    //}
    //    //if(_waitShivers >= cdDeSusto && shivers == true)
    //    //{
    //    //    shivers = false;
    //    //}

    //    //if (_scared == true && _waitscared >= 5)
    //    //{
    //    //    //_agent.speed = speedNormal;
    //    //    //_scared = false;

    //    //    StopScare();
    //    //}

    //    if (_doubt && _inPlace &&_waitDoubt>=2)
    //    {
    //        //_agent.speed = speedNormal;
    //        //_doubt = false;
    //        //_inPlace = false;
    //        //GetNewNode();
    //        //_agent.SetDestination(_actualNode.position);

    //        StopSearching();
    //    }

    //    //if (!_doubt) _waitDoubt = 0;


    //}
    #endregion

    protected virtual Transform GetNewNode(Transform lastNode = null)
    {
        Transform newNode = _navMeshNodes[Random.Range(1, _navMeshNodes.Count)];

        while(lastNode == newNode) 
        {
            newNode = _navMeshNodes[Random.Range(1, _navMeshNodes.Count)];
        }
        
        return newNode;
    }

    public virtual void GetScared()
    {
        //https://www.youtube.com/watch?v=eVrYbKBrI7o
    }

    protected virtual void StopScare()
    {
        
    }


    public virtual void GetShivers(AudioClip a)
    {
        
    }

    protected virtual void StopShivers()
    {
        _agent.speed = speedNormal;
    }

    public virtual void GetDoubt(Vector3 pos)
    {
        if (!_agent.enabled) return;
        //Debug.Log(" Duda de Npc");
        _doubt = true;

        //_audioSource.clip = doubtClip;
        //_audioSource.Play();

        _agent.speed = speedDoubt;

        _agent.SetDestination(pos);
        _searchingPos = pos;
    }

    protected void StartSearching()
    {
        _inPlace = true;
        _waitDoubt = 0;
    }

    protected void StopSearching()
    {
        //if(_anim != null)
        //{
        //    _anim.SetBool("Idle", false);
        //}
        _searchingTimer = 0;
        _agent.speed = speedNormal;
        _doubt = false;
        _inPlace = false;
        GetNewNode();
        _agent.SetDestination(_actualNode.position);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Npc.Remove(this);
    }
}
