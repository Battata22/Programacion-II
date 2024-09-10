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
    public float wait, waitscared;
    public bool shivers = false, _scared = false;
    AudioSource _audioSource;
    [SerializeField] AudioClip gritoClip;

    [SerializeField] Transform _actualNode;
    [SerializeField] List<Transform> _navMeshNodes = new();
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
        if((Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);

            //Debug.Log($"Nodo actua {_actualNode}");
        }

        wait += Time.deltaTime;
        waitscared += Time.deltaTime;

        if(wait >= tiempoDeSusto && shivers == true)
        {
            _agent.speed = 5;
        }
        if(wait >= cdDeSusto && shivers == true)
        {
            shivers = false;
        }

        if (_scared == true && waitscared >= 5)
        {
            _agent.speed = 5;
            _scared = false;
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
        _audioSource.clip = gritoClip;
        _audioSource.Play();
        waitscared = 0;
        GetNewNode(_actualNode);
        _scared = true;
        _agent.speed = 15f;
        _agent.SetDestination(_actualNode.position);
    }

    public void GetShivers()
    {
       wait = 0;
       _agent.speed = 0;
       shivers = true;
    }

    public void GetDoubt(Vector3 pos)
    {
        print("estamos en eso");
        //poner nodo en (pos)
        //hacer que el nodo que sigue el npc sea ese y bajarle la velocidad
        //_agent.speed = 2f;
        //una vez que llegue a la pos del nodo que se elimine ese nodo
        //que se quede quieta un par de segundos como investigando
        //_agent.speed = 0f;
        // if (pasaron 2s entonces _agent.speed = 5f)
        //que se le asigne el siguiente nodo a seguir con el orden
    }

    //miedo calculador
}
