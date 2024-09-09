using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luces : Obj_Interactuable
{
    [SerializeField] Light _luz;
    [SerializeField] bool on = true;
    AudioSource _audioSource;
    [SerializeField] AudioClip _clip;

    private void Awake()
    {
        _luz = GetComponentInChildren<Light>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip; 
    }

    public void LightSwitch()
    {
        if (on == true)
        {
            _luz.enabled = false;
            on = false;
            _audioSource.Play();
        }
        else if (on == false)
        {
            _luz.enabled = true;
            on = true;
            _audioSource.Play();
        }
    }

    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {
        
    }
}
