using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof (Rigidbody))]

public abstract class NPC : MonoBehaviour, IRoomDetectable
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
    //[SerializeField] public List<Transform> _testNodes = new();
    //protected Animator _anim;

    //public List<Transform> NavMeshNodes    
    //{ 
    //    get { return _navMeshNodes; }
    //    set { _navMeshNodes = value; }
    //}

    protected Vector3 _searchingPos;
    [SerializeField] protected bool _AIActive;

    protected NavMeshAgent _agent;

    protected Particulas _particulas;

    public int actualRoom;

    //REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK REWORK 
    //Rework para niveles con multiples salas activas a la vez
    [Header("<color=blue>TEST ROOMS INDIVIDUALES</color>")]
    [SerializeField] bool _useOwnNodes = false;
    public bool useOwnNode 
    { 
        get { return _useOwnNodes; } 
        protected set
        {
            _useOwnNodes = value;
            if (value)
            {
                GetOwnNodes();
            }
        } 
    }//geter setter, evita que algun script toque el valor de _useOwnNode
    [SerializeField] RoomTrigger[] _myRooms;
    [SerializeField] AINodeManager _nodeManager;

    protected virtual void Start()
    {

        GameManager.Instance.Npc.Add(this);
        _audioSource = GetComponentInChildren<AudioSource>();
        _particulas = GetComponentInChildren<Particulas>();

        //yield return new WaitForEndOfFrame();
        //_navMeshNodes.Clear();
        //_navMeshNodes = GameManager.Instance.AiNodes;
        //_testNodes = GameManager.Instance.AiNodes;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = speedNormal;
        //Initialize();
        //_actualNode = GetNewNode();
    }

    private void OnEnable()
    {
        if (useOwnNode)
            GetOwnNodes();
    }

    public void Initialize()
    {
        //_target = GameManager.Instance.Player.transform;
        //_agent.enabled = true;


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
        if (!useOwnNode)
        {

            Transform newNodeTest = GameManager.Instance.activeNodes[Random.Range(0, GameManager.Instance.activeNodes.Count)];

            while (lastNode == newNodeTest)
            {
                newNodeTest = GameManager.Instance.activeNodes[Random.Range(0, GameManager.Instance.activeNodes.Count)];
            }

            return newNodeTest;
        }
        else
        {
            Transform newNode = _navMeshNodes[Random.Range(1, _navMeshNodes.Count)];

            while (lastNode == newNode)
            {
                newNode = _navMeshNodes[Random.Range(1, _navMeshNodes.Count)];
            }

            return newNode;
        }
    }

    public virtual void GetScared(float a, Transform t = null)
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
        //_actualNode = GetNewNode();

        _actualNode = GetNewNode(_actualNode);
        _agent.SetDestination(_actualNode.position);

        //_agent.SetDestination(_actualNode.position);
    }

    protected virtual void OnDestroy()
    {
        GameManager.Instance.Npc.Remove(this);
    }

    public virtual void TurnOn() {
        Debug.Log("<color=red>TURN ON NO IMPLEMENTADO</color>");
    }

    public virtual void TurnOff() {
        Debug.Log("<color=red>TURN OFF NO IMPLEMENTADO</color>");

    }


    public virtual void SetRoom(int room)
    {
        actualRoom = room;
    }


    //public virtual void Slide()
    //{
    //    Debug.Log("Slide de NPC");
    //}

    void GetOwnNodes()
    {
        _navMeshNodes.Clear();

        foreach(var room in _myRooms)
        {
            _navMeshNodes.AddRange(_nodeManager.GetNodesOnList(room.roomIndex));
        }

    }

    public void StartUseOwnNode()
    {
        if (_myRooms.Length == 0 || _nodeManager == null)
        {
            Debug.Log($"<color=red>QUE HACES PELOTUDO?, falta asignar cosas xd </color>/n" +
                $"<color=green>Rooms asignadas {_myRooms.Length}| NodeManager {_nodeManager.name}</color>");
            return;
        }

        useOwnNode = true;

    }

    public void StopUseOwnNode()
    {
        useOwnNode = false;
    }
}
