using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody  ))]
public class Cat : NPC
{
    [Header("<color=#560833> Lucifer, Ruler of mankind </color>")]
    
    [SerializeField] Pickable _targetObject;
    [SerializeField] float _jumpCD, _jumpDis, _jumpForce, _dropDis;
    [SerializeField] bool _canJump, _onFloor, _searchObj;
    [SerializeField] LayerMask _mask, _floorMask;
    Rigidbody _rb;
    float _lastJump, _rbDrag;
    bool _antiSpam;

    
    

    protected override void Start()
    {
        base.Start();
        //yield return null;
        //StartCoroutine(CheckForObjects());
        _rb = GetComponent<Rigidbody>();
        _rbDrag = _rb.drag;
    }

    private void Update()
    {
        if (!_AIActive) return; 
        //if (_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();

        if (_agent.enabled   && (!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            //Debug.Log("<color=#26c5f0> LLege al destino </color>");

            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);
        }

        //if (_doubt)
        //    _searchingTimer += Time.deltaTime;
        //if (_searchingTimer > 12f) StopSearching();
        //if (_doubt && _inPlace && _waitDoubt >= 2)
        //{
        //    StopSearching();
        //}


        if (!_canJump && Time.time - _lastJump > _jumpCD)
        {
            _canJump = true;
            _searchObj = true;
        }

        if (_targetObject && _canJump)
        {
            //_agent.isStopped = true;
            //Imposible de despegar al gato del piso
            JumpToObject();
        }

        RaycastHit _floor;

        if(!_onFloor && Time.time - _lastJump > _jumpCD / 2)
        {
            OnFloor();
        }
        else if (!_onFloor && Time.time - _lastJump > 0.05f)
        {
            if (Physics.Raycast(transform.position, -transform.up, out _floor, 0.3f, LayerMask.GetMask("NoTras")))
            {
                OnFloor();
            }
        }        

        if (!_antiSpam && _searchObj)
        {
            StartCoroutine(CheckForObjects());
        }

        if(!_onFloor && _targetObject != null && Vector3.SqrMagnitude(transform.position - _targetObject.transform.position) <= (_dropDis * _dropDis))
        {
            _targetObject.Drop();
        }
    }

    void CheckObjects()
    {
        _targetObject = null;
        Collider[] _objs;
        //Debug.Log("Chequeando");
        _objs = Physics.OverlapSphere(transform.position, _jumpDis, _mask);
        foreach (Collider obj in _objs)
        {
            //Debug.Log($"<color=orange>Detectado {obj.name}</color>");
            if (obj.TryGetComponent<Pickable>(out Pickable p) && p.holding == true)
            {
                //Debug.Log($"<color=orange>Detectado {p.name}</color>");
                //Debug.Log("Encontrado");
                _targetObject = p;
                //Debug.Log($"<color=green>Target {_targetObject.name}</color>");
            }
        }
    }

    private IEnumerator CheckForObjects()
    {
        _antiSpam = true;
        
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        //Debug.Log("Funcionando");
        while (_searchObj)
        {
            yield return wait;
            //_targetObject = null;
            //Collider[] _objs;
            //Debug.Log("Chequeando");
            //_objs = Physics.OverlapSphere(transform.position, _jumpDis, _mask);
            //foreach (Collider obj in _objs)
            //{
            //    //Debug.Log($"<color=orange>Detectado {obj.name}</color>");
            //    if (obj.TryGetComponent<Pickable>(out Pickable p) && p.holding == true)
            //    {
            //        //Debug.Log($"<color=orange>Detectado {p.name}</color>");
            //        Debug.Log("Encontrado");
            //        _targetObject = p;
            //        Debug.Log($"<color=green>Target {_targetObject.name}</color>");
            //    }
            //}

            CheckObjects();
            
        }
        
        //Debug.Log("Stoped");
    }

    void JumpToObject()
    {
        var dir = (_targetObject.transform.position - transform.position).normalized;
        _agent.enabled = false;
        _canJump = false;
        _antiSpam=false;
        _rb.useGravity = true;
        _onFloor = false;
        _searchObj = false;
        //_rb.AddForce(transform.up * _jumpForce  , ForceMode.Impulse);
        _rb.drag = 0f;
        transform.forward = new Vector3(dir.x, 0, dir.z);
        _rb.AddForce(transform.up * _jumpForce* _rb.mass * 0.5f, ForceMode.Impulse);
        _rb.AddForce(dir * _jumpForce * _rb.mass, ForceMode.Impulse);

        //_targetObject.Drop();

        _targetObject = null;
        _lastJump = Time.time;
    }

    void OnFloor()
    {
        if (_onFloor) return;
        //StartCoroutine(CheckForObjects());
        _targetObject = null;
        _rb.velocity = Vector3.zero;
        _rb.drag = _rbDrag;
        _agent.enabled = true;
        _agent.SetDestination(_actualNode.position);
        _rb.useGravity = false;
        _onFloor = true;
    }

    public override void GetDoubt(Vector3 pos)
    {
        //base.GetDoubt(pos);
    }
}
