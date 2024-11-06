using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasEfectos : MonoBehaviour
{
    [SerializeField] ParticleSystem pSystem;
    Asustable asusScript;
    [SerializeField] bool activado = false, activadoRagdoll = false;
    [SerializeField] float duracion;
    [SerializeField] float wait;

    void Start()
    {

        pSystem = GetComponentInChildren<ParticleSystem>();
        asusScript = GetComponent<Asustable>();
        pSystem.Stop();

    }


    void Update()
    {
        if (activado && wait < duracion)
        {
            wait += Time.deltaTime;
        }
        else
        {
            wait = 0;
            PararParticulas();
        }

    }

    public void ActivarParticulas()
    {
        pSystem.gameObject.SetActive(true);
        pSystem.Play();
        activado = true;
    }

    public void PararParticulas()
    {
        pSystem.Stop();
        pSystem.gameObject.SetActive(false);
        activado = false;
    }

    //public void ActivarParticulasRagdoll()
    //{
    //    pSystem.gameObject.SetActive(true);
    //    pSystem.Play();
    //    activadoRagdoll = true;
    //    print("test act");
    //}

    //public void PararParticulasRagdoll()
    //{
    //    pSystem.Stop();
    //    pSystem.gameObject.SetActive(false);
    //    activadoRagdoll = false;
    //    print("test parar");
    //}
}
