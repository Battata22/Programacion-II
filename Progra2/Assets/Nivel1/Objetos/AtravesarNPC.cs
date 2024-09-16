using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtravesarNPC : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip shiver;
    [SerializeField] NPC npcScript;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        npcScript = GetComponentInParent<NPC>();
    }


    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            
            npcScript.GetShivers();
            npcScript._audioSource.clip = shiver;
            //npcScript._audioSource.Play();
            
        }
    }
}
