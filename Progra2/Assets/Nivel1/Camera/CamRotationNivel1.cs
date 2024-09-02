using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotationNivel1 : MonoBehaviour
{
    [Header("Sensibilidad")]
    [SerializeField] float _xSens;
    [SerializeField] float _ySens;

    //Inputs
    float _mouseX, _mouseY;

    float _xRotation, _yRotation;

    [SerializeField] Transform _playerOrientation;
    [SerializeField] Transform _meshOrientation;

    private void Start()
    {

        //centra y oculta mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Inputs
        _mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _xSens;
        _mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _ySens;

        //pasar inputs a la camara
        _yRotation += _mouseX;
        _xRotation -= _mouseY;

        //limita que no te pases de largo mirando arriba y abajo
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // Rotar camara o centro en nuestro caso
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _playerOrientation.rotation = Quaternion.Euler(0,_yRotation, 0);
        _meshOrientation.rotation = Quaternion.Euler(_xRotation,_yRotation,0);
    }
}
