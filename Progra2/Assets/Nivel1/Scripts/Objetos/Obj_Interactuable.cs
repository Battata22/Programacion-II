using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obj_Interactuable : MonoBehaviour 
{
    [SerializeField] protected Transform _camera;
    [SerializeField] protected float _cd, _lastInteract; //de momento no usado
    [SerializeField] protected float _speed; //del objeto volando en tu direccion
    [SerializeField] protected Transform _itemHolder; // punto al que va el objeto
    protected Rigidbody _rb;
    public bool mediano = false, grande = false, holding = false;
    public int lvlRequired;
    //{
    //    get { return lvlRequired; }
    //    protected set { lvlRequired = value; }
    //}//geter seter
    protected bool _canMove = false, _onAir = false;
    protected Collider[] _col;
    [SerializeField] protected Material _materialNormal, _materialFade;
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected LayerMask _dropLayers;

    protected Material _outLine;
    public virtual Material OutLine
    {
        get { return _outLine; }
        protected set { _outLine = value; }
    }
    protected float _OGthik;
    protected bool _outLineAntispam;

    //Material[] _mats;
    //Material _outLine, _fade;
    //float _OGthik, _OGalpha;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponents<Collider>();
        _renderer = GetComponent<Renderer>();
        if (grande) lvlRequired = 3;
        else if(mediano) lvlRequired = 2;
        else lvlRequired = 1;
        #region Comment
        //_mats = GetComponents<Material>();
        //foreach(var mat in _mats)
        //{
        //    if (mat.name == "M_Outline")
        //    {
        //        _outLine = mat;
        //        _OGthik = _outLine.GetFloat("Thickness");
        //        _outLine.SetFloat("Thickness", 0f);
        //    }
        //    else if (mat.name == "M_Fade")
        //    {
        //        _fade = mat;
        //        _OGalpha = _fade.GetColor("Base Color").a;
        //        _fade.SetFloat("Base Texture", 0f);
        //    }
        //}


        //_materialNormal = GetComponent<Material>(); 
        #endregion
    }

    public virtual void Interact(AudioSource _audio, AudioClip agarre, AudioClip error, int playerLevel)
    {
        _lastInteract = Time.time;
        _audio.clip = agarre;   
        _audio.PlayOneShot(agarre);
    }
    #region Comment
    //Vector3 dir = Vector3.zero;
    //protected void Movement(Transform _target)
    //{
    //    if(!holding) dir = (_target.position - transform.position);
    //    if (dir.sqrMagnitude > 0.2f)
    //    {
    //        transform.position += dir.normalized * _speed * Time.fixedDeltaTime;
    //    }
    //    else
    //    {

    //        GameManager.Instance.HandState.holding = true;
    //        GameManager.Instance.HandState.relax = false;
    //        GameManager.Instance.HandState.ChangeState();

    //        holding = true;
    //    }


    //    if(holding)
    //    {
    //        transform.position = _target.position;
    //    }

    //}
    #endregion
    public virtual void Throw(AudioSource _audio, AudioClip tirar)
    {
        if (holding == false)
        {
            return;
        }
        _lastInteract = Time.time;

        GameManager.Instance.HandState.holding = false;
        GameManager.Instance.HandState.pointing = false;
        GameManager.Instance.HandState.relax = true;
        GameManager.Instance.HandState.ChangeState();

        holding = false;
        _canMove = false;
        _rb.useGravity = true;
        if(grande == true)
        {
            _rb.AddForce(_camera.forward * 200f, ForceMode.Impulse);
            _rb.AddTorque(transform.up * 100f);
            _rb.AddTorque(transform.right * 100f);
        }
        else
        {
            _rb.AddForce(_camera.forward * 50f, ForceMode.Impulse);
            _rb.AddTorque(transform.up * 200f);
            _rb.AddTorque(transform.right * 200f);
        }

    }

    //public override void PlayMusic(AudioClip _audio1)
    //{
    //    base.PlayMusic(_audio1);
    //}
    public virtual void SlcFxOn()
    {
        //particleGen.Play();
        //parTime = Time.time;

        //shaders aca
        //Debug.Log("<Color=blue> Prendido</color>");
        OutLine.SetFloat("_Thickness", _OGthik);
        _outLineAntispam = false;
    }

    public virtual void SlcFxOff()
    {
        //particleGen.Stop();

        //sader aca
        if (_outLineAntispam) return;
        //Debug.Log("<Color=red> APAGADO </color>");
        OutLine.SetFloat("_Thickness", 0f);
        _outLineAntispam = true ;
    }

}
