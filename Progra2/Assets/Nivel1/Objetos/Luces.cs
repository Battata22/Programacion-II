using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luces : Obj_Interactuable
{
    //[SerializeField] Light _luz;
    [SerializeField] List<Light> _luces;
    [SerializeField] bool on = true;
    AudioSource _audioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] Chocamiento _chocamiento;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _chocamiento = GetComponent<Chocamiento>();
        _audioSource.clip = _clip; 
    }

    public void LightSwitch()
    {
        //on == true
        if (_luces[0].enabled == true)
        {
            for(int i = 0;  i < _luces.Count; i++)
            {
                _luces[i].enabled = false;
            }
            on = false;
            _audioSource.Play();
            _chocamiento.Choco(transform.position);
        }
        else if (_luces[0].enabled == false)
        {
            for (int i = 0; i < _luces.Count; i++)
            {
                _luces[i].enabled = true;
            }
            on = true;
            _audioSource.Play();
            _chocamiento.Choco(transform.position);
        }
    }

    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {
        
    }
}
