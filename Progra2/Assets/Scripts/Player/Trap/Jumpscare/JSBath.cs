using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class JSBath : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = GameManager.Instance.jumpscareBoo;
        gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = GameManager.Instance.AudioGroupSfx;
        gameObject.GetComponent<AudioSource>().Play();
    }

}
