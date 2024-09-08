using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] float _changeNodeDist = 0.5f;

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

            Debug.Log($"Nodo actua {_actualNode}");
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

    //miedo calculador
}
