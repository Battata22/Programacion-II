using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linea : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] LineRenderer linea;

    void Start()
    {
        linea = GetComponent<LineRenderer>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Asustable>(out Asustable script))
        {
            script.GetStun(clip);
            print("paso");
        }
    }
}
