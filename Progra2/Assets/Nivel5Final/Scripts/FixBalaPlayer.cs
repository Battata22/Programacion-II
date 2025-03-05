using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixBalaPlayer : MonoBehaviour
{
    [SerializeField] GameObject padre;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && other.gameObject.name != "LimiteAspirado")
        {
            print(other.gameObject.name);
            Destroy(padre);
        }
    }
}
