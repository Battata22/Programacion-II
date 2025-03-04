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

    [SerializeField] AudioSource _audSource;
    [SerializeField] AudioClip _glassClip;

    [SerializeField] GameObject[] _glassPices;

    public event DelegateType.VoidDelegate OnBroken = delegate {};
    public event DelegateType.VoidDelegate OnDamaged = delegate { };

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audSource = GetComponent<AudioSource>();
        
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

        _audSource.PlayOneShot(_glassClip);
        //Destroy(this);
        //disable script

        UnChingoDeVidrio();
    }

    void UnChingoDeVidrio()
    {
        foreach (var pice in _glassPices)
        {
            pice.SetActive(true);

            if(pice.TryGetComponent<Rigidbody>(out var rb))
            {

                var x = Random.Range(-1f, 1f);
                var y = Random.Range(-1f, 1f);
                var z = Random.Range(-1f, 1f);
                var merda = new Vector3(x, y, z).normalized;

                rb.AddForce(merda * Random.Range(0.5f,6f), ForceMode.VelocityChange);
            }
        }
    }

}
