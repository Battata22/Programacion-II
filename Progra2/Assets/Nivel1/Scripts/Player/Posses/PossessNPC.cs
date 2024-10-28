using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PossessNPC : MonoBehaviour
{
    [SerializeField] float _radius, _rayDistance;
    [SerializeField] LayerMask _detectableMask;

    Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.blue);

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, _radius, transform.forward, out hit, _rayDistance, _detectableMask))
        {
            var asustable = hit.transform.GetComponent<Asustable>();
            if (Input.GetKeyDown(KeyCode.Q) && hit.transform.TryGetComponent<IPossessable>(out IPossessable target) && asustable.stuned)
            {
                target.GetPossess();
                _player.StartPossession();
            }
        }
    }
}
