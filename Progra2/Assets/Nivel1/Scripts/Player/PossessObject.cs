using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PossessObject : MonoBehaviour
{
    float _xAxis, _zAxis;
    float _speed;
    public Player _player;
    Rigidbody _rb;

    Vector3 _dir;

    public Player player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = true;
        _speed = 1000f;
        _rb.freezeRotation = true;
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndPossession();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit floor;
        //if (_xAxis != 0 || _zAxis != 0)
        //{
        //}
        //else if(Physics.Raycast(transform.position, -transform.up, out floor, 0.1f, LayerMask.GetMask("NoTras")))
        //    _rb.velocity = Vector3.zero;    
        Movement(_xAxis, _zAxis);
        player.PossessMovement();
    }

    void Movement(float xAxis, float zAxis)
    {
        RaycastHit hitR, hitL, hitF, hitB;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        if (Physics.SphereCast(pos, 0.25f, transform.right, out hitR, 0.52f, LayerMask.GetMask("NoTras")) && xAxis > 0)
        {
            //Debug.Log("<color=ellow> Wall Detected R </color>");
            return;
        }
        if (Physics.SphereCast(pos, 0.25f, -transform.right, out hitL, 0.52f, LayerMask.GetMask("NoTras")) && xAxis < 0)
        {
            //Debug.Log("<color=ellow> Wall Detected L </color>");
            return;
        }
        if (Physics.SphereCast(pos, 0.25f, transform.forward, out hitF, 0.52f, LayerMask.GetMask("NoTras")) && zAxis > 0)
        {
            //Debug.Log("<color=ellow> Wall Detected F </color>");
            return;
        }
        if (Physics.SphereCast(pos, 0.25f, -transform.forward, out hitB, 0.52f, LayerMask.GetMask("NoTras")) && zAxis < 0)
        {
            //Debug.Log("<color=ellow> Wall Detected B </color>");
            return;
        }
        _dir = (transform.right * xAxis + transform.forward * zAxis).normalized;
        //_rb.position += _dir * _speed * Time.fixedDeltaTime;
        _rb.AddForce(_dir * _speed * Time.fixedDeltaTime, ForceMode.Force);
    }

    void EndPossession()
    {
        //Devolver al jugador

        //set pos jugador en el objeto
        //devolver el camera center al player
        //activar el player
        //destruir este script
        _rb.freezeRotation = false;
        player.EndPossession();

        Destroy(this);
    }

}
