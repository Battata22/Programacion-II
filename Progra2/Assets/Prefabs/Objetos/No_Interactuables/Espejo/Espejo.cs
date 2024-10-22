using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espejo : MonoBehaviour
{
    [SerializeField] GameObject escrito;
    [SerializeField] Animator animator;
    public bool activo = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Escritura()
    {
        animator.SetBool("Activado", true);
        activo = true;
    }
}
