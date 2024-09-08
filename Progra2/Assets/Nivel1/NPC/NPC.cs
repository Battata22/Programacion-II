using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] float _changeNodeDist = 0.5f;

    Transform _target, _actualNode;
    List<Transform> _navMeshNodes = new();
    public List<Transform> NavMeshNodes 
    { 
        get { return _navMeshNodes; }
        set { _navMeshNodes = value; }
    }

    NavMeshAgent _agent;
    private void Awake()
    {
        //GameManager.Instance.NPC.Add(this);
    }
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameManager.Instance.NPC.Add(this);
        _actualNode = GetNewNode();
    }

    public void Initialized()
    {
        _target = GameManager.Instance.Player.transform;

        _actualNode = GetNewNode();

        _agent.SetDestination(_actualNode.position);
    }

    private void Update()
    {
        if(Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist))
        {
            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);

            Debug.Log($"Nodo actua {_actualNode}");
        }
    }

    Transform GetNewNode(Transform lastNode = null)
    {
        Transform newNode = _navMeshNodes[Random.Range(0, _navMeshNodes.Count)];

        while(lastNode == newNode) 
        {
            newNode = _navMeshNodes[Random.Range(0, _navMeshNodes.Count)];
        }

        return newNode;
    }
}
