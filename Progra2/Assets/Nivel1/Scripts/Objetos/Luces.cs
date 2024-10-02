using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Luces : Obj_Interactuable
{
    //[SerializeField] Light _luz;
    [SerializeField] List<Light> _luces;
    [SerializeField] bool on = true;
    AudioSource _audioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] Chocamiento _chocamiento;

    //protected Material /*_outLine, */_fade;
    //public override Material OutLine

    //{
    //    get { return _outLine; }
    //    protected set { _outLine = value; }
    //}

    //public Material Fade
    //{
    //    get { return _fade; }
    //    protected set { _fade = value; }
    //}
    //protected float _OGthik;
    //protected Color _OGcolor;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _chocamiento = GetComponent<Chocamiento>();
        _renderer = GetComponent<Renderer>();
        _audioSource.clip = _clip;

        //if (_renderer != null)
        //{
        //    foreach (var mat in _renderer.materials)
        //    {
        //        //print(mat.name);
        //        if (mat.name == "M_Outline (Instance)")
        //        {
        //            OutLine = mat;
        //            _OGthik = OutLine.GetFloat("_Thickness");
        //            OutLine.SetFloat("_Thickness", 0f);
        //        }
        //        else if (mat.name == "M_Fade (Instance)")
        //        {
        //            Fade = mat;
        //            _OGcolor = Fade.GetColor("_baseColor");
        //            var noAlpha = new Color(_OGcolor.r, _OGcolor.g, _OGcolor.b, 0f);
        //            Fade.SetColor("_baseColor", noAlpha);//     = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 0f);   

        //        }
        //    }
        //}
    }

    private void LateUpdate()
    {
        //if (Fade == null) return;

        //if (holding)
        //{
        //    //transform.position = _target.position;
        //    if (Fade.GetColor("_baseColor") != _OGcolor)
        //    {
        //        Fade.SetColor("_baseColor", _OGcolor);
        //    }
        //}
        //else
        //{
        //    if (Fade.GetColor("_baseColor") == _OGcolor)
        //    {
        //        Fade.SetColor("_baseColor", new Color(_OGcolor.r, _OGcolor.g, _OGcolor.b, 0f));
        //    }
        //}
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
            _chocamiento.ChocoSonoro(transform.position);
        }
        else if (_luces[0].enabled == false)
        {
            for (int i = 0; i < _luces.Count; i++)
            {
                _luces[i].enabled = true;
            }
            on = true;
            _audioSource.Play();
            _chocamiento.ChocoSonoro(transform.position);
        }
    }

    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {
        
    }
}
