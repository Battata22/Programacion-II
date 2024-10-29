using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessBehavior : MonoBehaviour
{
    float _xAxis, _zAxis;
    [SerializeField] float _speed;
    [SerializeField] Animator _anim;
    public Player player;
    Rigidbody _rb;

    Vector3 _dir;

    IPossessable _possessable;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _possessable = GetComponent<IPossessable>();
        _rb.useGravity = true;
        _rb.freezeRotation = true;
        player = GameManager.Instance.Player;
        _speed = 1200;
    }

    private void Update()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndPossession();
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _anim.SetFloat("zAxis", 0f);
            _anim.SetBool("Walking", true);
            _anim.SetBool("Idle", false);
        }
        else
        {
            _anim.SetFloat("zAxis", 0f);
            _anim.SetBool("Idle", true);
            _anim.SetBool("Walking", false);
        }
        
    }

    private void FixedUpdate()
    {
        player.PossessMovement(transform);
        Movement();
    }

    void Movement()
    {
        var dir = (_xAxis * transform.right + _zAxis * transform.forward).normalized;
        _rb.AddForce(dir * _speed * Time.fixedDeltaTime, ForceMode.Acceleration); // += dir * _speed * Time.fixedDeltaTime;
        if (_rb.velocity.magnitude > _speed)
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _speed);
        }
    }

    void EndPossession()
    {
        player.EndPossession();

        var npc = transform.GetComponent<Asustable>();
        npc.enabled = true;
        npc.EndPossession();

        Destroy(this);
    }

}
