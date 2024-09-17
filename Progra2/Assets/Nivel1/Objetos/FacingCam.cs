using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCam : MonoBehaviour
{
    Transform _camera;
    Vector3 _dir = new();
    // Update is called once per frame
    void Update()
    {
        if (_camera == null) _camera = GameManager.Instance.Camera.transform;

        transform.LookAt(_camera.position);
    }
}
