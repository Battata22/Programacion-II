using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PewPew : MonoBehaviour
{
    [SerializeField] bool semi = true;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootHere;


    void Update()
    {
        if (semi)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

    }

    void Shoot()
    {
        Instantiate(bullet, shootHere.transform.position, quaternion.identity);

    }
}
