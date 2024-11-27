using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BoxBehavior : MonoBehaviour, IFlamable
{
    bool OnFire = false;
    [SerializeField] ParticleSystem _fireGen;

    private void Start()
    {
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particleSystem in particleSystems)
        {
            if(particleSystem.gameObject.name == "Particle HumoCAJAS")
            {
                _fireGen = particleSystem;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!OnFire) return;
        if(collision.gameObject.TryGetComponent<IFlamable>(out var flamable))
        {
            flamable.SetOnFire();
        }
    }


    public void ExtinguishFire()
    {
        _fireGen.Stop();
    }

    public void SetOnFire()
    {
        //Activar particulas de humo
        if (OnFire) return;
        Debug.Log($"<color=red> Set on fire </color>");

        OnFire = true;
        _fireGen.Play();

        GameManager.Instance.Rociadores.OnRociadoresActive += ExtinguishFire;
        GameManager.Instance.Rociadores.objectsOnFire++;
    }

}
