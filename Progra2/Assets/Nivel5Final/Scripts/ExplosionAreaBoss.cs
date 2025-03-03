using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExplosionAreaBoss : MonoBehaviour
{
    [SerializeField] float speed, tamano;
    [SerializeField] AudioSource Asource;
    [SerializeField] AudioClip explosionClip;

    private void Start()
    {
        Asource = GetComponent<AudioSource>();
        Asource.clip = explosionClip;
        Asource.Play();
    }
    private void Update()
    {
        transform.localScale += transform.localScale / transform.localScale.x * Time.deltaTime * speed;
        if (transform.localScale.x > tamano) Destroy(gameObject);//transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);   
    }
}
