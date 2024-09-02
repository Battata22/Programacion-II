using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNivel1 : MonoBehaviour
{
    [Header("Cosas necesarias")]
    Rigidbody _rb;
    Vector3 Spawn = new Vector3(0f, 1f, 0f);


    [Header("Movement")]
    [SerializeField] float _speed;
    [SerializeField] float salto;
    float _xAxis,_zAxis;
    Vector3 _dir = new();
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        LifeSaver(transform.position.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0)
        {
            Movement(_xAxis, _zAxis);
        }
    }

    void Movement(float xAxis, float zAxis)
    {
        _dir = (transform.right * xAxis + transform.forward * zAxis).normalized;

        transform.position += _dir * _speed * Time.deltaTime;
    }

    void Jump()
    {
        _rb.AddForce(transform.up * salto, ForceMode.Impulse);
    }

    void LifeSaver(float _dis)
    {
        if(_dis <= -15)
        {
            transform.position = Spawn;
        }
    }

}
