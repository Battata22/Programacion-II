using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Pickable , IWhaterContainer
{
    [Header("<color=blue>Bucket</color>")]
    bool _hasWather;
    [SerializeField] MeshFilter _myMesh;
    [SerializeField] Mesh _conAgua;
    [SerializeField] Mesh _sinAgua;

    [SerializeField] ParticleSystem _particulas;

    public event DelegateType.VoidDelegate OnGetWather = delegate { };
    public event DelegateType.VoidDelegate OnLostWather = delegate { };


    public bool CheckWather()
    {
        return _hasWather;
    }

    //protected override void OnCollisionEnter(Collision collision)
    //{
    //    base.OnCollisionEnter(collision);

    //    SalpicaAgua();
    //}

    void SalpicaAgua()
    {
        if (!_hasWather) return;

        var pingo = Instantiate(_particulas, transform.position, Quaternion.identity);

    }

    public void GetWather()
    {
        Debug.Log($"<color=blue>{name} LLeno de agua</color>");
        _hasWather = true;

        _myMesh.mesh = _conAgua;

        OnGetWather();
    }

    public void LostWather()
    {
        Debug.Log($"<color=red>{name} vacio</color>");
        _hasWather = false;

        _myMesh.mesh = _sinAgua;


        OnLostWather();
    }
}
