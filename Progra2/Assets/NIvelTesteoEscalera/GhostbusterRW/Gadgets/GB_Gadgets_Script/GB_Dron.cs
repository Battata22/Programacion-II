using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class GB_Dron : GB_Gadget
{
    NavMeshAgent _agent;
    [Header("Logic")]
    [SerializeField] float _changeNodeDist;
    Transform _actualNode = null;

    bool _IAACtive = false;

    [Header("Stats")]
    [SerializeField] int _maxHp;
    [SerializeField] float _speed;
    int _hp;

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
            Debug.Log("AHHHHHHHH");
            //Invoke("SetNewDestination", 1f);
            SetNewDestination(_actualNode);
        }
    }

    public override void Break()
    {
        if (_isBroken) return;

        _isBroken = true;
        _agent.speed = 0f;

        _myOwner.AddToRepairList(this);
        OnBreak();
    }

    public override void GetDamage(int dmgAmount = 1)
    {
        _hp -= dmgAmount;

        if (_hp <= 0)
            Break();
    }

    public override void Repair()
    {
        if (!_isBroken) return;

        _hp = _maxHp;

        _isBroken = false;

        SetNewDestination(_actualNode);
        _agent.speed = _speed;

        OnRepair();
    }

    protected Transform GetNewNode(Transform lastNode = null)
    {
        Debug.Log("ENTRE A NEW NODE");

        Transform newNodeTest = GameManager.Instance.activeNodes[Random.Range(0, GameManager.Instance.activeNodes.Count)];

        Debug.Log("ANTES DEL WHILE");

        while (lastNode == newNodeTest)
        {
            newNodeTest = GameManager.Instance.activeNodes[Random.Range(0, GameManager.Instance.activeNodes.Count)];
        }

        Debug.Log("SALI DEL WHILE");


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

        Debug.Log($"<color=cyan> Nuevo Destino Elegido {_actualNode.name} </color>");
    }
}
