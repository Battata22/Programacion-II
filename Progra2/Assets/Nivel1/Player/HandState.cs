using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class HandState : MonoBehaviour
{
    [SerializeField] public bool pointing = false, relax = false, holding = false;
    [SerializeField] Mesh _relaxMesh, _pointingMesh, _holdingMesh, _tensMesh;
    MeshFilter _filter;
    Vector3 _up;

    private void Start()
    {
        GameManager.Instance.HandState = this;
        _filter = gameObject.GetComponent<MeshFilter>();

        _up = transform.up;

        ChangeState();
    }

    public void ChangeState()
    {
        if (holding && !relax)
        {
            //  transform.up = _up;
            
            _filter.mesh = _holdingMesh;
        }
        if (pointing && !holding && !relax)
        {
            
            _filter.mesh = _pointingMesh;
        }
        if (!pointing && !holding && relax)
        {
            //transform.up = _up;
            
            _filter.mesh = _relaxMesh;
        }
        if(!pointing && !holding && !relax)
        {
            //transform.up = _up;
            
            _filter.mesh= _relaxMesh;
        }

    }
}
