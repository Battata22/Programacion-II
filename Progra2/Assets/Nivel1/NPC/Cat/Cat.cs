using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody  ))]
public class Cat : NPC
{
    [Header("<color=#560833> Lucifer, Ruler of mankind </color>")]
    
    [SerializeField] Pickable _targetObject;
    [SerializeField] float _jumpCD, _jumpDis, _jumpForce;
    [SerializeField] bool _canJump;
    [SerializeField] LayerMask _mask;
    Rigidbody _rb;
    float _lastJump;

    Collider[] _objs;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(CheckForObjects());
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_AIActive) return;
        //if (_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();

        if ((!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            //Debug.Log("<color=#26c5f0> LLege al destino </color>");

            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);
        }

        if (!_canJump && Time.time - _lastJump > _jumpCD)
        {
            _canJump = true;
        }

        if (_targetObject && _canJump)
        {
            //_agent.isStopped = true;
            //Imposible de despegar al gato del piso
            JumpToObject();
        }
    }

    private IEnumerator CheckForObjects()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (_canJump)
        {
            yield return wait;
            _objs = Physics.OverlapSphere(transform.position, _jumpDis, _mask);
            foreach (Collider obj in _objs)
            {
                //Debug.Log($"<color=orange>Detectado {obj.name}</color>");
                if (obj.TryGetComponent<Pickable>(out Pickable p) && p.holding == true)
                {
                    //Debug.Log($"<color=orange>Detectado {p.name}</color>");
                    _targetObject = p;
                    Debug.Log($"<color=green>Target {_targetObject.name}</color>");
                }
            }
        }
    }

    void JumpToObject()
    {
        var dir = (_targetObject.transform.position - transform.position).normalized;
        _canJump = false;
        _rb.AddForce(transform.up * _jumpForce  , ForceMode.Impulse);
        _rb.AddForce(dir * _jumpForce, ForceMode.Impulse);
        _lastJump = Time.time;
    }
}
