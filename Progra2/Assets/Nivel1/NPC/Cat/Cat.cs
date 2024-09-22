using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : NPC
{
    [Header("<color=#560833> Lucifer, Ruler of mankind </color>")]
    [SerializeField] int something;

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
    }
}
