using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViejaDurmiente : MonoBehaviour
{
    [SerializeField] GameObject gato, abuela, perro;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Pickable>(out Pickable pickScript))
        {
            if (pickScript._trowed == true)
            {
                StartGame();
            } 
        }
    }

    void StartGame()
    {
        gato.SetActive(true);
        perro.SetActive(true);
        abuela.SetActive(true);
        gameObject.SetActive(false);
    }
}
