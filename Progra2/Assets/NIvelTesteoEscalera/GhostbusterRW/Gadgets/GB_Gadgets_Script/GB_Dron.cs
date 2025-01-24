//using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class GB_Dron : GB_Gadget
{
    NavMeshAgent _agent;
    [Header("Logic")]
    [SerializeField] float _changeNodeDist;
    Transform _actualNode = null;
    [SerializeField] GB_Flashbang _pulsePrefab;
    [SerializeField] Transform _pulseOrigin;
    [SerializeField, Tooltip("False = GetAngry, True = GetDoubt")] bool _doDoubt;

    bool _IAACtive = false;

    [Header("Stats")]
    [SerializeField] int _maxHp;
    [SerializeField] float _speed;
    int _hp;

    [Header("Use GB nodes")]
    [SerializeField] bool _useOwnNode = false;


    public override void Initialize(Ghostbuster newOwner)
    {
        gameObject.SetActive(true);
        //TurnOn();
        base.Initialize(newOwner);
        _useOwnNode = newOwner.useOwnNode;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _hp = _maxHp;
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SetNewDestination();
        _IAACtive = true;
    }

    private void Update()
    {
        if (!_IAACtive) return;
        if(!_isBroken && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist))
        {
            //Debug.Log("AHHHHHHHH");
            //Invoke("SetNewDestination", 1f);
            SetNewDestination(_actualNode);
            SpawnPulse(_myOwner);
        }
    }

    public override void Break()
    {
        if (_isBroken) return;

        Debug.Log($"<color=red> AHHHH CARALHO, VOCE E MOITO RUIM FHILO DA PUTA </color>");

        _isBroken = true;
        //_agent.speed = 0f;
        TurnOff();

        _myOwner.AddToRepairList(this);
        OnBreak();
    }

    public override void GetDamage(int dmgAmount = 1)
    {
        Debug.Log($"<color=red> OWCh, digo digo BIP! </color>");

        _hp -= dmgAmount;

        if (_hp <= 0)
            Break();
    }

    public override void Repair()
    {
        if (!_isBroken) return;

        _hp = _maxHp;

        _isBroken = false;

        //SetNewDestination(_actualNode);
        //_agent.speed = _speed;
        TurnOn();

        OnRepair();
    }

    protected Transform GetNewNode(Transform lastNode = null)
    {
        if (_useOwnNode)
        {
            Transform newOwnNode = _myOwner.NavMeshNodes[Random.Range(0, _myOwner.NavMeshNodes.Count)];

            //Debug.Log("ANTES DEL WHILE");

            while (lastNode == newOwnNode)
            {
                newOwnNode = _myOwner.NavMeshNodes[Random.Range(0, _myOwner.NavMeshNodes.Count)];
            }

            //Debug.Log("SALI DEL WHILE");


            return newOwnNode;
        }

        //Debug.Log("ENTRE A NEW NODE");

        Transform newNodeTest = GameManager.Instance.activeNodes[Random.Range(0, GameManager.Instance.activeNodes.Count)];

        //Debug.Log("ANTES DEL WHILE");

        while (lastNode == newNodeTest)
        {
            newNodeTest = GameManager.Instance.activeNodes[Random.Range(0, GameManager.Instance.activeNodes.Count)];
        }

        //Debug.Log("SALI DEL WHILE");


        return newNodeTest;
    }

    void SetNewDestination(Transform lastDest = null)
    {

        //Debug.Log("ENTRE A SET DEST");

        if (lastDest != null)
        {
            //Debug.Log("LAST != NULL");

            _actualNode = GetNewNode(lastDest);
        }
        else
        {
            //Debug.Log("LAST = NULL");

            _actualNode = GetNewNode();
        }

        //Debug.Log($" NODO ELEGIDO {_actualNode.name}");


        _agent.SetDestination(_actualNode.position);

        //Debug.Log($"<color=cyan> Nuevo Destino Elegido {_actualNode.name} </color>");
    }
    void SpawnPulse(Ghostbuster owner)
    {
        var gadget = Instantiate(_pulsePrefab, _pulseOrigin.position, _pulseOrigin.rotation);

        //if (owner != null)
            gadget.Initialize(owner, _doDoubt);
    }

    void TurnOff()
    {
        transform.GetComponent<Collider>().enabled = false;
        //_AIActive = false;
        _agent.speed = 0f;
        _agent.enabled = false;
    }

    void TurnOn()
    {
        transform.GetComponent<Collider>().enabled = true;
        //_AIActive = true;
        _agent.enabled = true;
        _agent.speed = _speed;
        if (_actualNode != null)
            _agent.SetDestination(_actualNode.position);
    }
}
