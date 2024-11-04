using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirAudioRuptura : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<AudioClip> clipList= new List<AudioClip>();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (clipList != null)
        {
            int random = Random.Range(0, clipList.Count);
            audioSource.clip = clipList[random];
        }
        audioSource.Play();
        Invoke("SelfDestruct", 4f);
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

}
