using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class prueba : MonoBehaviour
{
    [SerializeField] float vel, rotVel;
    Rigidbody rb;
    [SerializeField] GameObject objRotar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        var dir = objRotar.transform.position - transform.position;
        rb.AddForce(dir * rotVel * (1 / Vector3.Distance(objRotar.transform.position, transform.position)) * Time.deltaTime, ForceMode.Impulse);
        rb.position += new Vector3(vel * Time.fixedDeltaTime, 0, 0);
    }
}
