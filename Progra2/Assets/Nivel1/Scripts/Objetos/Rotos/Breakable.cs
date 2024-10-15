using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Pickable
{
    [SerializeField] GameObject tazaOK;
    [SerializeField] GameObject tazaBroken;

    BoxCollider boxCollider;
    void Awake()
    {
        tazaOK.SetActive(true);
        tazaBroken.SetActive(false);

        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Break();
    }
    private void Break()
    {
        tazaOK.SetActive(false);
        tazaBroken.SetActive(true);
    }
}
