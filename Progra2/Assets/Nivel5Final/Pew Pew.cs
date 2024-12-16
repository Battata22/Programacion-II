using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPew : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootHere;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        print("piu piu");
        Instantiate(bullet, shootHere.transform);
    }
}
