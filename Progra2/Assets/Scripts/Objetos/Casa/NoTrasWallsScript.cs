using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTrasWallsScript : MonoBehaviour
{

    [SerializeField] AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            source.Stop();
            source.Play();
        }
    }
}
