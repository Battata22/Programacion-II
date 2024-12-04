using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _pelvisBone;
    [SerializeField] Rigidbody _boneToImpulseRb;
    EnableRagdoll _myOwner;
    Rigidbody _pelvisRb;
    Vector3 _direction;
    float _mult;

    bool _impulsePelvis;

    public void Initialize(EnableRagdoll newOwner, Vector3 newDir, float newMult, bool newState)
    {

        _pelvisRb = _pelvisBone.GetComponent<Rigidbody>();
        _myOwner = newOwner;
        transform.forward = _myOwner.transform.forward;
        _myOwner.OnDeactivate += Rip;
        _direction = newDir;
        _mult = newMult;

        _impulsePelvis = newState;

        ApplyForce();
    }

    //private void FixedUpdate()
    //{
    //    var dir = (new Vector3(_pelvisBone.transform.position.x, _myOwner.transform.position.y, _pelvisBone.transform.position.z) - _myOwner.transform.position).normalized;
    //    _myOwner._rb.AddForce(dir * Time.fixedDeltaTime * 5, ForceMode.Acceleration);
    //}

    void ApplyForce()
    {
        if (!_impulsePelvis)
            _boneToImpulseRb.AddForce(_direction * _mult, ForceMode.VelocityChange);
        else
            _pelvisRb.AddForce(_direction * _mult, ForceMode.VelocityChange);
    }

    void Rip()
    {
        _myOwner.transform.position = _pelvisBone.transform.position;
        //_myOwner.transform.position = new Vector3(_pelvisBone.transform.position.x, _myOwner.transform.position.x, _pelvisBone.transform.position.z);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _myOwner.OnDeactivate -= Rip;
    }


}
