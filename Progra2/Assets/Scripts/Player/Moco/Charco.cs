using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charco : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip caminarNpc, reboteItem;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        Destroy(gameObject, 4.65f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Asustable>(out Asustable asusScript))
        {
            asusScript.StartSlow();
            source.clip = caminarNpc;
            //source.loop = true;
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Asustable>(out Asustable asusScript))
        {
            asusScript.StopSlow();
            //source.loop = false;
            source.Stop();
        }
    }
}
