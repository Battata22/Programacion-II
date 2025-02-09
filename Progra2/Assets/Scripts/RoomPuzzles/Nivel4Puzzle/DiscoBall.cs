using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public class DiscoBall : MonoBehaviour, IDamageable
{
    [Header("<color=#9fedd8>Balls</color>")]
    [SerializeField] int _maxHp;
    int _hp;
    [SerializeField] float _dmgCd;
    bool _imbecil = false; // in englis "invincible"

    Rigidbody _rb;

    public event DelegateType.VoidDelegate OnDamageTaken = delegate { };
    public event DelegateType.VoidDelegate OnBreak = delegate { };

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _hp = _maxHp;
    }

    public void GetDamage(int dmgAmount = 1)
    {
        if (_imbecil) return;
        _imbecil = true;
        StartCoroutine(SosImbecil(_dmgCd));

        _hp -= dmgAmount;

        OnDamageTaken();
        if( _hp <= 0 )
        {
            _hp = 0;
            Drop();
        }
    }

    IEnumerator SosImbecil(float wait)
    {
        yield return new WaitForSeconds(wait);

        _imbecil = false;
    }

    void Drop()
    {
        //dropea
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.constraints = RigidbodyConstraints.FreezePositionZ;
        _rb.constraints = RigidbodyConstraints.FreezePositionX;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<Piso>(out var piso))
        {
            Break();
        }
    }

    void Break()
    {

        //cambiar mesh
        Debug.Log("<color=#ed9fd7>NYAA</color>");

        OnBreak();
    }


}
