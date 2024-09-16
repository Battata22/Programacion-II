using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostbuster : NPC
{
    [Header("<color=red> Ghostbuster </color>")]
    [SerializeField] GB_FOV _gbFov;
    [SerializeField] float _angerRange, _angerTime , _attackRange, _attackDuration;
    float _waitAnger;

    [SerializeField] Player _target;

    bool _lastState;
    [SerializeField] bool _angry, _isAttacking;

    //NO FUNCIONA DEL TODO

    protected override void Start()
    {
        base.Start();
        //_target = GameManager.Instance.Player;
        _gbFov = GetComponent<GB_FOV>();
    }

    private void Update()
    {
        if (!_AIActive) return;
        if(_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();

        if ((!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);
        }

        if (_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist))
        {
            _agent.speed = 0;

            if (!_inPlace)
            {
                StartSearching();
            }
        }

        if(_gbFov.hasLOS != _lastState)
        {
            _lastState = _gbFov.hasLOS;
            if (_gbFov.hasLOS)
            {
                Debug.Log("Te veo");
                GetAngry();
            }
            else
            {
                Debug.Log("Donde andas?");
            }
        }

        if(_angry && Time.time - _waitAnger > _angerTime)
        {
            StopAnger();
            //Debug.Log("<color=red> Despues de stop </color>");
            _agent.SetDestination(GetNewNode(_actualNode).position);
            //Debug.Log("<color=green> Despues de set destination </color>");
        }
        if (!_isAttacking && _gbFov.hasLOS && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_attackRange * _attackRange))
        {
            //Attack();
        }

    }

    public override void GetDoubt(Vector3 pos)
    {
        //activar duda
        Debug.Log("<color=yellow> Escuche algo </color>");
    }

    public override void GetScare()
    {
        //Activar Anger supongo
        Debug.Log("<color=red> YA TE VOY A AGARRAR </color>");
    }

    void GetAngry()
    {

        _angry = true;
        _waitAnger = Time.time;
        StartCoroutine(ChaseTarget());
    }

    void StopAnger()
    {
        _angry = false;
        StopCoroutine(ChaseTarget());
        
    }

    void Attack()
    {
        //Activar modo Luigi
        //Los objetos libianos cercanos tambien seria succionados? idea
        _isAttacking = true; 
        StopCoroutine(ChaseTarget());
    }

    private IEnumerator ChaseTarget()
    {
        //consigue pos de Gus cada medio segundo
        //Al salir de persecucion no vuelve a patruyar
        //https://www.youtube.com/watch?v=5T5BY1j2MkE no abrir

        //if(!_angry) yield return null;
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (_angry)
        {
            yield return wait;
            //_actualNode = (_target.transform.position.x, transform.position.y , _target.transform.position.z);
            _agent.SetDestination(new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z));
        }
        //yield return null;
    }
}
