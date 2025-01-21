using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CanillaBanhoDepa : SpecialObject
{
    [SerializeField] Espejo _espejo;
    [SerializeField] Asustable _target;
    [SerializeField] Bath _bath;
    [SerializeField] ParticleSystem niebla;

    private void Start()
    {
        //GameManager.Instance.pasoActual = 1;
    }

    bool inPos = false, trapActive = false;

    private void Update()
    {
        if (trapActive && !inPos && Vector3.SqrMagnitude(_target.transform.position - transform.position) < (2f * 2f))
        {
            inPos = true;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.TryGetComponent<Pickable>(out var obj) && obj._trowed)
    //    {
    //        ActivarCanilla();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Pickable>(out var obj) && obj._trowed)
        {
            ActivarCanilla();
        }
    }

    void ActivarCanilla()
    {
        Debug.Log("Activar sonido");
        //Debug.Log("Activar Particulas de neblina");
        niebla.gameObject.SetActive(true);
        //trapActive = true;

        CreateTrap();

        GameManager.Instance.pasoActual = 7;

    }

    protected override void ObjectAbility(Transform origin)
    {
        _target.GetDoubt(transform.position, roomIndex);

        trapActive = true;

        StartCoroutine(WaitToScare());
    }

    IEnumerator WaitToScare()
    {
        //trapActive = true;

        while (inPos == false)
        {
            yield return null;
        }

        

        yield return new WaitForSeconds(0.5f);

        _espejo.Escritura();
        //Debug.Log($"<color=#ff00ff> IGNACIOOOOOOO </color>");

        yield return new WaitForSeconds(0.2f);

        _target.GetScared(1f,roomIndex);
        GameManager.Instance.pasoActual = 2;

        Destroy(_trap);
        //llamar bath create
        _bath.CreateTrap();
    }
}
