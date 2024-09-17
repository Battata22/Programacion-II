using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtravesarNPC : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip shiver;
    [SerializeField] Asustable asustableScript;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        asustableScript = GetComponentInParent<Asustable>();
    }


    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            
            asustableScript.GetShivers();
            asustableScript._audioSource.clip = shiver;
            asustableScript._audioSource.Play();
            
        }
    }
}
