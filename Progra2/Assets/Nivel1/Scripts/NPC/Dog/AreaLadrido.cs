using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLadrido : MonoBehaviour
{
    [SerializeField] DogScript dogScript;
    void Start()
    {
        dogScript = GetComponentInParent<DogScript>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            //print("adentro");
            dogScript.Ladrido();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            //print("afuera");
            dogScript.StopLadrido();
        }
    }
}
