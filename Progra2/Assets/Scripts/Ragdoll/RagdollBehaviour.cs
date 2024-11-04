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
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _myOwner.OnDeactivate -= Rip;
    }


}
