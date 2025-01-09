using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogToy : MonoBehaviour
{
    [SerializeField] Chocamiento _myChoco;
    [SerializeField] DogToyArea _area;

    private void Awake()
    {
        _myChoco = GetComponent<Chocamiento>();
    }

    private void Start()
    {
        _myChoco.OnChocoActive += CallDog;
    }

    void CallDog()
    {
        //Spawn Area para llamar al perro
        var obj = Instantiate(_area, transform.position, Quaternion.identity);
        obj.Initialize(transform);
        Debug.Log($"<color=green> VENI PERRO LA CONCHA DE TU MADRE </color>");
    }
}
