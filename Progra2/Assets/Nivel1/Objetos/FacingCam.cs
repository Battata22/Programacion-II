using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCam : MonoBehaviour
{
    [SerializeField] Transform _camera;
    Vector3 _dir = new();
    // Update is called once per frame
    void LateUpdate()
    {
        transform.up = -_camera.forward;
    }
}
