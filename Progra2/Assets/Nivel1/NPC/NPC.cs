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
    float wait;
    public bool shivers = false;

    [SerializeField] Transform _actualNode;
    [SerializeField] List<Transform> _navMeshNodes = new();
    public List<Transform> NavMeshNodes    
    { 
        get { return _navMeshNodes; }
        set { _navMeshNodes = value; }
    }

    NavMeshAgent _agent;
    bool _scared = false;

    void Start()
    {
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
        if((Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);

            //Debug.Log($"Nodo actua {_actualNode}");
        }

        wait += Time.deltaTime;

        if(wait >= tiempoDeSusto && shivers == true)
        {
            _agent.speed = 5;
        }
        if(wait >= cdDeSusto && shivers == true)
        {
            shivers = false;
        }

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
        GetNewNode(_actualNode);
        _scared = true;
        _agent.speed = 10f;

        _agent.SetDestination(_actualNode.position);
    }

    public void GetShivers()
    {
       wait = 0;
       _agent.speed = 0;
       shivers = true;
    }

    //miedo calculador
}
