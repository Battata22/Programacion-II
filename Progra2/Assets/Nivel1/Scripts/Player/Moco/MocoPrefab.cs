using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocoPrefab : MonoBehaviour
{
    Collider self;
    [SerializeField] AudioClip clip;

    void Start()
    {
        self = GetComponent<Collider>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Asustable>(out Asustable script))
        {
            script.GetMoco(clip);
        } 
    }
}
