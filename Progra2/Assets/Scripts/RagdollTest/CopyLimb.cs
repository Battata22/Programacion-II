using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    [SerializeField] Transform[] _allLimb;
    [SerializeField] Transform _copyLimb;
    [SerializeField] bool _mirror, _test;


    public ConfigurableJoint _cj;
    
    Vector3 _originalRot;
    float _originalStrenght;

    private void Awake()
    {
        _cj = GetComponent<ConfigurableJoint>();
        foreach (Transform t in _allLimb)
        {
            if (t.name == this.name)
            {
                _copyLimb = t;
            }
        }
        _originalRot = _copyLimb.localEulerAngles;
        _originalStrenght = _cj.slerpDrive.positionSpring;
        JointDrive n = _cj.slerpDrive;
        Debug.Log(n.positionSpring);
        n.positionSpring = 0f;
        Debug.Log(n.positionSpring);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StartCoroutine(GiveStrenght());
        }

        SetRotation();

    }

    float ModJointStrg(ref float coso, float newValue)
    {
        coso = newValue;
        return coso;
    }

    void SetRotation()
    {
        if (!_mirror)
        {
            if (!_test)
            {
                _cj.targetRotation = Quaternion.Euler(_originalRot - _copyLimb.localEulerAngles);
            }
            else
            {
                _cj.targetRotation = Quaternion.Euler(_copyLimb.localEulerAngles - _originalRot);
            }
        }
        else
        {
            if (!_test)
            {
                _cj.targetRotation = Quaternion.Inverse(Quaternion.Euler(_originalRot - _copyLimb.localEulerAngles));
            }
            else
            {
                _cj.targetRotation = Quaternion.Inverse(Quaternion.Euler(_copyLimb.localEulerAngles - _originalRot));
            }
            //_cj.targetRotation = Quaternion.Inverse(Quaternion.Euler(_originalRot - _copyLimb.localEulerAngles));
        }
    }
    
    IEnumerator GiveStrenght()
    {
        Debug.Log("<color=red> ACTIVANDO AUTODESTRUCCION </color>");
        var n = _cj.slerpDrive;
        //n.positionSpring = 1000f;

        float time = 0;
        //float test;

        while (_cj.slerpDrive.positionSpring < _originalStrenght)
        {
            time += Time.deltaTime;
            //n.positionSpring = (Mathf.Lerp(0, _originalStrenght, time));
            //_cj.slerpDrive.positionSpring = (_originalStrenght * 10);
            yield return null;
        }
        Debug.Log(n.positionSpring);
    }
}
