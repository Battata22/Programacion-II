//using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Chocamiento))]
public class Pickable : Obj_Interactuable
{
    bool _pickedUp, _trowed;
    public PickUp pickUpScript;
    public ParticleSystem particleGen;
    float _parMaxTime = 5f;
    public float parTime;
    NavMeshObstacle _navObstacle;

    //Material _originalMaterial;

    protected Material /*_outLine,*/ _fade;
    public override Material OutLine
    { 
        get { return _outLine;} 
        protected set { _outLine = value; } 
    }
    public Material Fade
    { 
        get { return _fade; }
        protected set { _fade = value; } 
    }
    //protected float _OGthik;
    protected Color _OGcolor;

    protected virtual void Start()
    {      
        //_camera = GameManager.Instance.Camera.transform;
        //_itemHolder = GameManager.Instance.ItemHolde;
        //pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        //_materialNormal = GetComponent<Material>();
        if(_rb == null) _rb = GetComponent<Rigidbody>();
        _speed = 10f;
        _cd = 1;
        if (mediano)
        {
            _cd = 2;
            _rb.mass = 4f;          
        }
        if (grande)
        {
            _cd = 3;
            _rb.mass = 7f;
        }
        if ((mediano || grande) && !GetComponent<NavMeshObstacle>())
        { 
            this.AddComponent<NavMeshObstacle>();
            
        }
        _navObstacle = GetComponent<NavMeshObstacle>();

        if (_renderer != null)
        {
            foreach (var mat in _renderer.materials)
            {
                //print(mat.name);
                if (mat.name == "M_Outline (Instance)")
                {
                    OutLine = mat;
                    _OGthik = OutLine.GetFloat("_Thickness");
                    OutLine.SetFloat("_Thickness", 0f);
                }
                else if (mat.name == "M_Fade (Instance)")
                {
                    Fade = mat;
                    _OGcolor = Fade.GetColor("_baseColor");
                    var noAlpha = new Color(_OGcolor.r, _OGcolor.g, _OGcolor.b, 0f);
                    Fade.SetColor("_baseColor", noAlpha);//     = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 0f);   
                    
                }
            }
        }

        _dropLayers = GameManager.Instance.DropLayers;
        particleGen = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        //if(_materialNormal == null) _materialNormal = GetComponent<Material>();
        if (_camera == null) _camera = GameManager.Instance.Camera.transform;
        if(_itemHolder == null) _itemHolder = GameManager.Instance.ItemHolde;
        if(pickUpScript == null) pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (holding && Input.GetMouseButtonDown(1))
        {
            Throw(pickUpScript._audioSource, pickUpScript.tirar);
            pickUpScript.sosteniendoBool = false;
            pickUpScript._audioSource.loop = false;
        }

        if (holding && Input.GetMouseButtonDown(0))
        {
            Drop();
            pickUpScript.sosteniendoBool = false;
            pickUpScript._audioSource.loop = false;
        }

        //if (holding && !_trowed)
        //{
        //    CheckForDrop();
        //}

        if(particleGen != null && Time.time - parTime > _parMaxTime)
        {
            particleGen.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Movement(_itemHolder);
        }
    }

    private void LateUpdate()
    {
        if (Fade == null) return;
        
        if (holding)
        {
            //transform.position = _target.position;
            if (Fade.GetColor("_baseColor") != _OGcolor)
            {
                Fade.SetColor("_baseColor", _OGcolor);
            }
        }
        else
        {
            if (Fade.GetColor("_baseColor") == _OGcolor)
            {
                Fade.SetColor("_baseColor", new Color(_OGcolor.r, _OGcolor.g, _OGcolor.b, 0f));
            }
        }
    }

    protected Vector3 dir = Vector3.zero;
    protected void Movement(Transform _target)
    {
        if (!holding) dir = (_target.position - transform.position);
        if (dir.sqrMagnitude > 0.2f)
        {
            transform.position += dir.normalized * _speed * Time.fixedDeltaTime;
        }
        else
        {

            GameManager.Instance.HandState.holding = true;
            GameManager.Instance.HandState.relax = false;
            GameManager.Instance.HandState.ChangeState();

            holding = true;
        }


        if (holding)
        {
            transform.position = _target.position;
            if (Fade.GetColor("_baseColor") != _OGcolor)
            {
                Fade.SetColor("_baseColor", _OGcolor);
            }
        }
        else
        {
            if (Fade.GetColor("_baseColor") == _OGcolor)
            {
                Fade.SetColor("_baseColor", new Color(_OGcolor.r, _OGcolor.g, _OGcolor.b, 0f));
            }
        }

    }

    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error)
    {

        if (pickUpScript.isHolding == false && Time.time - _lastInteract > _cd)
        {
            _lastInteract = Time.time;
            base.Interact(_audio, agarre, error);
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
            _canMove = true;
            _pickedUp = true;
            pickUpScript.isHolding = true;
            _onAir = true;
            if(_navObstacle != null) _navObstacle.enabled = false;
            //_col.enabled = false;
            foreach(Collider c in _col)
                c.isTrigger = true;
            _rb.constraints = RigidbodyConstraints.None;
            pickUpScript.esperaragarre = 0;
            _renderer = GetComponent<Renderer>();
            _renderer.material = _materialFade;
        }


    }

    public override void Throw(AudioSource _audio, AudioClip arrojar)
    { 
        base.Throw(pickUpScript._audioSource, pickUpScript.tirar);
        _trowed = true;
        //_col.enabled = true;
        if (_navObstacle != null) _navObstacle.enabled = true;
        if (particleGen)
            particleGen.Stop();
        foreach(Collider c in _col)
            c.isTrigger = false;
        _pickedUp = false;
        pickUpScript.isHolding = false;
        _onAir = true;
        _renderer.material = _materialNormal;
        _audio.clip = arrojar;
        _audio.Play();

    }

    public void Drop()
    {
        //if (holding == false) return;
        _lastInteract= Time.time;

        GameManager.Instance.HandState.holding = false;
        GameManager.Instance.HandState.pointing = false;
        GameManager.Instance.HandState.relax = true;
        GameManager.Instance.HandState.ChangeState();
        if (_navObstacle != null) _navObstacle.enabled = true;
        if (particleGen)
            particleGen.Stop();
        //_col.enabled = true;
        foreach (Collider c in _col) 
            c.isTrigger = false;
        _pickedUp = false;
        pickUpScript.isHolding = false;
        _onAir = false;
        holding = false;
        _canMove = false;
        _rb.useGravity = true;
        _renderer.material = _materialNormal;
        GameManager.Instance.Player.GetComponent<AudioSource>().Stop();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //if (canMove) Drop();
        if(_onAir)
        {
            //Collider[] hitObjs = Physics.OverlapSphere(transform.position, 0.5f, _layerMask);
            if (_trowed && TryGetComponent<Chocamiento>(out Chocamiento choc))
            {
                //Chocamiento choc = GetComponent<Chocamiento>();
                //if (TryGetComponent<Chocamiento>(out Chocamiento choc))//choc != null && 
                //{
                //    choc.Choco(transform.position);
                //    onAir = false;
                //}
                choc.Choco(transform.position);
                _onAir = false;
                _trowed = false;
            }
            //else if (hitObjs.length != 0)//collision.gameobject.layer != 7
            //{
            //    Drop();
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.layer == 31 ||  other.gameObject.layer == 8) && holding)// 31 wall y 8 NoTras
        {
            Drop();
        }
    }

    Collider[] hitObjs;
    void CheckForDrop()
    {
        Collider[] hitObjs = Physics.OverlapSphere(transform.position, 0.5f, _dropLayers);
        if (hitObjs.Length != 0)//collision.gameObject.layer != 7
        {
            Drop();
        }
    }

    public override void SlcFxOn()
    {
        //particleGen.Play();
        //parTime = Time.time;

        //shaders aca
        //Debug.Log("<Color=blue> Prendido</color>");
        OutLine.SetFloat("_Thickness", _OGthik);
    }

    public override void SlcFxOff()
    {
        //particleGen.Stop();

        //sader aca
        //Debug.Log("<Color=red> APAGADO </color>");
        OutLine.SetFloat("_Thickness", 0f);
    }

}
