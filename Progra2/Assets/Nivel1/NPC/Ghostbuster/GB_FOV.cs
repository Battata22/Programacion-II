using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_FOV : MonoBehaviour
{
    //Codigo sacado de este tutorial , ctrl + click para abrirlo
    //https://www.youtube.com/watch?v=j1-OyLo77ss

    [SerializeField] float _radius;
    [Range(0,360)]
    [SerializeField] float _angle;

    Player _target;

    [SerializeField] LayerMask _targetMask, _obstructionMask;

    [SerializeField] public bool hasLOS;

    private void Start()
    {
        _target = GameManager.Instance.Player;
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                    hasLOS = true;
                else
                    hasLOS = false;
            }
            else
            {
                hasLOS = false;
            }
        }
        else if (hasLOS)
            hasLOS = false;
    }
}
