using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] Animator _anim;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        GameManager.Instance.AnimPuerta = _anim;
    }


    void Update()
    {
        
    }

    public void Llego()
    {

    }
}
