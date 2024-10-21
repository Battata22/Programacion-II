using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Luces : Obj_Interactuable
{
    //[SerializeField] Light _luz;
    [SerializeField] List<Light> _luces;
    [SerializeField] bool on = true, rotas = false;
    AudioSource _audioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] Chocamiento _chocamiento;
    int random, usos;
    [SerializeField] int minPos, maxPos;
    [SerializeField] Luces switchPar;

    public override Material OutLine
    {
        get { return _outLine; }
        protected set { _outLine = value; }
    }

    #region Comment
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
    #endregion

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _chocamiento = GetComponent<Chocamiento>();
        _renderer = GetComponent<Renderer>();
        _audioSource.clip = _clip;
        #region Comment
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
        #endregion
    }
    private void Start()
    {
        random = Random.Range(minPos, maxPos);

        if (_renderer != null)
        {
            foreach (var mat in _renderer.materials)
            {
                //print(mat.name);
                if (mat.name == "M_OutlineL (Instance)")
                {
                    OutLine = mat;
                    _OGthik = OutLine.GetFloat("_Thickness");
                    OutLine.SetFloat("_Thickness", -0.02f);
                }
            }
        }

    }

    private void Update()
    {
        if (usos >= random)
        {
            rotas = true;
            Bluetooth();
        }

    }

    public void LightSwitch()
    {
        if (!rotas)
        {
            usos++;

            if (on)
            {
                Apagado();
            }
            else
            {
                Encendido();
            }
            _audioSource.Play();
            _chocamiento.ChocoSonoro(transform.position);
        }
        else
        {
            Apagado();
        }
    }

    public void Apagado()
    {
        for (int i = 0; i < _luces.Count; i++)
        {
            _luces[i].enabled = false;
        }
        on = false;
    }

    public void Encendido()
    {
        for (int i = 0; i < _luces.Count; i++)
        {
            _luces[i].enabled = true;
        }
        on = true;
    }

    public void Bluetooth()
    {
        if(switchPar != null)
        {
            switchPar.rotas = true;
        }
    }
}