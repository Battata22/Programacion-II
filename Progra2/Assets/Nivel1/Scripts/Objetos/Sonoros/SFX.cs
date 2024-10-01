using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : Pickable
{
    public AudioSource _audioSource;
    [SerializeField] protected bool isPlaying = false, clip = false;
    protected int random1;
    Chocamiento _chocamiento;

    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _col = GetComponents<Collider>();
        _chocamiento = GetComponent<Chocamiento>();
    }

    protected override void Start()
    {
        _renderer = GetComponent<Renderer>();
        base.Start();
        //print(_renderer.name);
        //if (_renderer != null)
        //{
        //    foreach (var mat in _renderer.materials)
        //    {
        //        print(mat.name);
        //        if (mat.name == "M_Outline (Material)")
        //        {
        //            OutLine = mat;
        //            _OGthik = OutLine.GetFloat("_Thickness");
        //            OutLine.SetFloat("_Thickness", 0f);
        //        }
        //        else if (mat.name == "M_Fade (Material)")
        //        {
        //            Fade = mat;
        //            _OGcolor = Fade.GetColor("_baseColor");
        //            var noAlpha = new Color(_OGcolor.r, _OGcolor.g, _OGcolor.b, 0f);
        //            Fade.SetColor("_baseColor", noAlpha);//     = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 0f);   

        //        }
        //    }
        //}
        //_audioSource = GetComponent<AudioSource>();
    }


    public virtual void PlayMusic(AudioClip _audio1)
    {
        if (isPlaying == false)
        {
            isPlaying = true;
            if (clip == false)
            {
                clip = true;
                _audioSource.clip = _audio1;
            }
            _audioSource.Play();
        }
        else if (isPlaying == true)
        {
            isPlaying = false;
            _audioSource.Pause();
        }
        _chocamiento.ChocoSonoro(transform.position);
    }
}
