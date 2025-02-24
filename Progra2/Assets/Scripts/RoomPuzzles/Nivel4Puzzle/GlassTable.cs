using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class GlassTable : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] int _hitNeeded;
    [SerializeField] Mesh _meshRoto;
    int _hitCount = 0;

    public event DelegateType.VoidDelegate OnBroken = delegate {};
    public event DelegateType.VoidDelegate OnDamaged = delegate { };

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<Asustable>(out var asus) && asus.sliding)
        {
            GetDamage();
        }
    }

    void GetDamage()
    {
        _hitCount++;
        OnDamaged();

        if(_hitCount >= _hitNeeded)
        {
            BreakObject();
        }
    }

    void BreakObject()
    {
        //Change model
        Debug.Log($"<color=red>Mesa rota</color>");
        OnBroken();

        GetComponent<MeshFilter>().mesh = _meshRoto;
        //Destroy(this);
        //disable script
    }
}
