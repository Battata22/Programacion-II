using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RandomDir : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1)));
        rb.AddTorque(new Vector3(Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1), Random.Range(-600, 600 + 1)));
    }

}
