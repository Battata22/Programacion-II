using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] public Vector3 point;
    [SerializeField] Transform _target;

    public bool usePoint=false;

    private void LateUpdate()
    {
        //el punto se lo da el CamDistance
        if (usePoint)
        {
            transform.position = point;
        }
        else
        {
            transform.position = _target.position;
        }
        
        transform.forward = _target.forward;
    }
}
