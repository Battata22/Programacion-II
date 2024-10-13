using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : Pickable
{

    //public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error, int playerLevel)
    //{
    //    Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
    //    base.Interact(_audio, agarre, error, playerLevel);
    //    Debug.Log(" AHHHHHHHHHHHHHHHHHHHHHHHH");
    //}

    PlungerHead _head;

    public bool OnAir 
    {
        get { return _onAir; }
        private set { } 
    }
    public Rigidbody Rb
    {
        get { return _rb; }
        set { _rb = value; }
    }

    Transform _ogPerent;
    public Transform fakeParent;

    [SerializeField] public Transform actualParent;

    public bool stuck = false;

    public delegate void DelegateVoidInt(int number);
    public event DelegateVoidInt OnStuck;

    protected override void Start()
    {
        _ogPerent = transform.parent;
        base.Start();
        _head = GetComponentInChildren<PlungerHead>();
        _head.Initialize(this);
    }

    protected override void Update()
    {
        base.Update();
        if (stuck)
            StuckMovement();
    }

    void StuckMovement()
    {
        transform.position = new Vector3(fakeParent.position.x,1.8f ,fakeParent.position.z);
        //transform.up = fakeParent.position.normalized;
    }
    public override void Interact(AudioSource _audio, AudioClip agarre, AudioClip error, int playerLevel)
    {
        base.Interact(_audio, agarre, error, playerLevel);
        stuck = false;
        gameObject.layer = 7;
        transform.SetParent(_ogPerent);
    }

    public void Stuck()
    {
        if (OnStuck != null)
            OnStuck(1);
    }
    //public override void Throw(AudioSource _audio, AudioClip arrojar)
    //{
    //    if (holding == false)
    //    {
    //        return;
    //    }
    //    _lastInteract = Time.time;

    //    GameManager.Instance.HandState.holding = false;
    //    GameManager.Instance.HandState.pointing = false;
    //    GameManager.Instance.HandState.relax = true;
    //    GameManager.Instance.HandState.ChangeState();

    //    holding = false;
    //    _canMove = false;
    //    _rb.useGravity = true;

    //    _rb.AddForce(_camera.forward * 50f, ForceMode.Impulse);

    //    _trowed = true;
    //    //_col.enabled = true;
    //    //if (_navObstacle != null) _navObstacle.enabled = true;
    //    if (particleGen)
    //        particleGen.Stop();
    //    foreach (Collider c in _col)
    //        c.isTrigger = false;
    //    if (_navObstacle != null)
    //    {
    //        //Debug.Log("AHHHHHHHHH de tirar");
    //        StartCoroutine(ActivateNavObstacle());
    //    }
    //    _pickedUp = false;
    //    pickUpScript.isHolding = false;
    //    _onAir = true;
    //    _renderer.material = _materialNormal;
    //    _audio.clip = arrojar;
    //    _audio.Play();

    //    if (trailGen != null)
    //        trailGen.Play();
    //    else
    //    {
    //        var newTrail = Instantiate(GameManager.Instance.TrailGen, transform);
    //        trailGen = newTrail.GetComponent<ParticleSystem>();
    //    }

    //    transform.up = transform.forward; //Quaternion.Euler(180,0,0);
    //}

    //protected override void OnCollisionEnter(Collision collision)
    //{
    //    if (_onAir)
    //    {
    //        _rb.useGravity = false;
    //        _rb.constraints = RigidbodyConstraints.FreezeAll;
    //    }
    //    base.OnCollisionEnter(collision);
    //}
}
