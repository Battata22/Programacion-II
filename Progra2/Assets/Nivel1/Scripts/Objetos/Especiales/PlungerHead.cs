using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlungerHead : MonoBehaviour
{
    Plunger _parent;
    NPC _npc;
    bool onAir = false;
    GameObject[] _bones; 
   
    public void Initialize(Plunger newParent)
    {
        _parent = newParent;
        GetComponent<Collider>().enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_parent.OnAir)
        {
            _parent.Rb.useGravity = false;
            //_parent.Rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_parent.OnAir && other.gameObject.layer != 0)
        {
            //_parent.Rb.constraints = RigidbodyConstraints.FreezeAll;
            if (other.TryGetComponent<NPC>(out _npc))
            {
                //_parent.transform.SetParent(_npc.transform);
                _parent.Rb.useGravity = false;
                _parent.fakeParent = other.transform;
                _parent.stuck = true;
                _parent.Rb.velocity = Vector3.zero;
                _parent.Rb.angularVelocity = Vector3.zero;
                _parent.gameObject.layer = 12;
                _parent.Stuck();
                //_parent.transform.localPosition = new Vector3(0, 2, 0);
            }
            //else
            //{
            //    _parent.transform.SetParent(other.transform);
            //    //_parent.transform.localPosition = Vector3.zero;
            //}
            _parent.actualParent = other.transform;
        }
    }
}
