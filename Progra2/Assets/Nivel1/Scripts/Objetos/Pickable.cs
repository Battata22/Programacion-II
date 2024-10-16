//using System.Drawing;
using System.Collections;
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
    public bool _pickedUp, _trowed, rompible = false;
    public PickUp pickUpScript;
    public ParticleSystem particleGen, trailGen;
    protected float _parMaxTime = 5f;
    public float parTime;
    [SerializeField]protected NavMeshObstacle _navObstacle;

    [SerializeField] protected float _scareAmount;

    //public delegate void DelegateEventVoid();
    //public event DelegateEventVoid OnThrow, OnDrop;
    //GameObject newTrail;

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

    Rompible rompscript;

    protected virtual void Start()
    {
        rompscript = GetComponent<Rompible>();
        //_camera = GameManager.Instance.Camera.transform;
        //_itemHolder = GameManager.Instance.ItemHolde;
        //pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        //_materialNormal = GetComponent<Material>();
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        _speed = 10f;
        _cd = 1;
        if (!mediano && !grande)
        {
            GameManager.Instance.Player.NerfLvl1 += NerfObj;
        }
        if (mediano)
        {
            _cd = 2;
            _rb.mass = 4f;
            GameManager.Instance.Player.NerfLvl2 += NerfObj;
        }
        if (grande)
        {
            _cd = 3;
            _rb.mass = 20f;
        }
        if (mediano || grande)
        { 
            if (!GetComponent<NavMeshObstacle>())
                this.AddComponent<NavMeshObstacle>();
            _navObstacle = GetComponent<NavMeshObstacle>();
            _navObstacle.carving = true;
            _navObstacle.carvingMoveThreshold = 0.1f;
            _navObstacle.carvingTimeToStationary = 0.4f;
            _navObstacle.carveOnlyStationary = true;
        }

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

        _scareAmount = 1f;
        _dropLayers = GameManager.Instance.DropLayers;
        particleGen = GetComponentInChildren<ParticleSystem>();
    }

    protected virtual void Update()
    {
        //if(_materialNormal == null) _materialNormal = GetComponent<Material>();
        if (_camera == null) _camera = GameManager.Instance.Camera.transform;
        if(_itemHolder == null) _itemHolder = GameManager.Instance.ItemHolde;
        if(pickUpScript == null) pickUpScript = GameManager.Instance.Player.GetComponentInChildren<PickUp>();
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (holding && Input.GetMouseButtonDown(1))
        {
            Throw(pickUpScript._audioSource, pickUpScript.tirar);
            if (GameManager.Instance.Tutorial != null && GameManager.Instance.Tutorial.throwTuto)
            {
                GameManager.Instance.Tutorial.EndThrow();
            }
            pickUpScript.sosteniendoBool = false;
            pickUpScript._audioSource.loop = false;
        }

        if (holding && Input.GetMouseButtonDown(0))
        {
            Drop();
            if (GameManager.Instance.Tutorial != null && GameManager.Instance.Tutorial.dropTuto)
            {
                GameManager.Instance.Tutorial.EndDrop();
            }
            pickUpScript.sosteniendoBool = false;
            pickUpScript._audioSource.loop = false;
        }

        //if (holding && !_trowed)
        //{
        //    CheckForDrop();
        //}

        //if(particleGen != null && Time.time - parTime > _parMaxTime)
        //{
        //    particleGen.Stop();
        //}
    }

    protected void FixedUpdate()
    {
        if (_canMove)
        {
            Movement(_itemHolder);
        }
    }

    protected void LateUpdate()
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
        else if (!holding)
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

    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error,int playerLevel)
    {

        if (pickUpScript.isHolding == false && Time.time - _lastInteract > _cd)
        {
            if (!(playerLevel >= lvlRequired))
            {
                //Debug.Log("<color=yellow> Nivel Insuficiente</color>");
                return;
            }
            base.Interact(_audio, agarre, error, playerLevel);
            _lastInteract = Time.time;

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
        //if (_navObstacle != null) _navObstacle.enabled = true;
        if (particleGen)
            particleGen.Stop();
        foreach(Collider c in _col)
            c.isTrigger = false;
        if (_navObstacle != null)
        {
            //Debug.Log("AHHHHHHHHH de tirar");
            StartCoroutine(ActivateNavObstacle());
        }
        _pickedUp = false;
        pickUpScript.isHolding = false;
        _onAir = true;
        _renderer.material = _materialNormal;
        _audio.clip = arrojar;
        _audio.Play();

        if (trailGen != null)
            trailGen.Play();
        else
        {
            var newTrail = Instantiate(GameManager.Instance.TrailGen, transform);
            trailGen = newTrail.GetComponent<ParticleSystem>();
        }
    }

    public void Drop()
    {
        //if (holding == false) return;
        _lastInteract= Time.time;

        GameManager.Instance.HandState.holding = false;
        GameManager.Instance.HandState.pointing = false;
        GameManager.Instance.HandState.relax = true;
        GameManager.Instance.HandState.ChangeState();
        if (_navObstacle != null)
        {
            //_navObstacle.enabled = true;
            //Debug.Log("AHHHHHHHHH de soltar");
            StartCoroutine(ActivateNavObstacle());
        }
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

    
    protected virtual void OnCollisionEnter(Collision collision)
    {
        //if (canMove) Drop();
        if(_onAir)
        {
            //Collider[] hitObjs = Physics.OverlapSphere(transform.position, 0.5f, _layerMask);
            if (_trowed && TryGetComponent<Chocamiento>(out Chocamiento choc))
            {
                #region comment
                //Chocamiento choc = GetComponent<Chocamiento>();
                //if (TryGetComponent<Chocamiento>(out Chocamiento choc))//choc != null && 
                //{
                //    choc.Choco(transform.position);
                //    onAir = false;
                //}
                //if (_navObstacle != null && collision.gameObject.layer == 8)
                //{
                //    //_navObstacle.enabled = true;
                //    ActivateNavObstacle();
                //}
                #endregion

                choc.scareAmount = _scareAmount;
                choc.Choco(transform.position);
                _onAir = false;
                _trowed = false;
                trailGen.Stop();
                if (rompible == true)
                {
                    rompscript.Rompe();
                    Destroy(gameObject);
                }
            }
            //else if (hitObjs.length != 0)//collision.gameobject.layer != 7
            //{
            //    Drop();
            //}
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == 31 || other.gameObject.layer == 8) && holding)// 31 wall y 8 NoTras
        {
            Drop();
        }
    }

    //Collider[] hitObjs;
    //void CheckForDrop()
    //{
    //    Collider[] hitObjs = Physics.OverlapSphere(transform.position, 0.5f, _dropLayers);
    //    if (hitObjs.Length != 0)//collision.gameObject.layer != 7
    //    {
    //        Drop();
    //    }
    //}

    public override void SlcFxOn()
    {
        //particleGen.Play();
        //parTime = Time.time;

        //shaders aca
        //Debug.Log("<Color=blue> Prendido</color>");
        OutLine.SetFloat("_Thickness", _OGthik);
        particleGen.Play();
        parTime = Time.time;
    }

    public override void SlcFxOff()
    {
        //particleGen.Stop();

        //sader aca
        //Debug.Log("<Color=red> APAGADO </color>");
        OutLine.SetFloat("_Thickness", 0f);
        particleGen.Stop(); 
    }

    public virtual void NerfObj(float num = 0.5f)
    {
        Debug.Log("<color=red> Objeto nerfeado </color>");
        _scareAmount *= num;
    }
    protected IEnumerator ActivateNavObstacle()
    {
        Debug.Log("<color=blue> LLAMADO A ACTIVAR </color>");
        yield return new WaitForSeconds(2f);
        if (holding || _onAir || _navObstacle.enabled)
        {
            Debug.Log("<color=red> NO ACTIVAR </color>");
        }
        else
        {
            Debug.Log("<color=green> ACTIVADO </color>");
            _navObstacle.enabled = true;
        }
    }
}
