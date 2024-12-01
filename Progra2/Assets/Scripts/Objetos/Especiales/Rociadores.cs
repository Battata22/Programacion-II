using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rociadores : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _aguaGen;
    [SerializeField] public int objectsOnFire;
    [SerializeField] public int MaxFIreNeeded;

    public event DelegateType.VoidDelegate OnRociadoresActive = delegate { };

    bool activo = false;

    private void Start()
    {
        GameManager.Instance.Rociadores = this;
    }

    private void Update()
    {
        if (!activo && objectsOnFire >= MaxFIreNeeded)
        {
            ActivarRociadores();

        }
    }

    void ActivarRociadores()
    {
        //Debug.Log("<color=blue> ROCIADOREES ACTIVOS </color>");
        activo = true;
        foreach (var obj in _aguaGen)
        {
            obj.Play();
        }


        OnRociadoresActive();
    }


}
