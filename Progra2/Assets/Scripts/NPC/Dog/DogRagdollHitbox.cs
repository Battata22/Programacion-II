using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DogRagdollHitbox : MonoBehaviour
{
    [SerializeField] Collider _myHitbox;
    public bool active
    {
        get 
        { 
            return _myHitbox.enabled; 
        }       
        set
        {
            if (value)
            {
                ActivateHitbox();
            }
            else
            {
                DeactivateHitbox();
            }
        }
    }

    private void Awake()
    {
        active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //doshit
        if(other.gameObject.TryGetComponent<IRagdoll>(out var ragdoll))
        {
            GetEmPrincess(ragdoll, other.transform);
        }
    }

    public void ActivateHitbox()
    {

        _myHitbox.enabled = true;
        Debug.Log($"<color=green> Hitbox State {_myHitbox.enabled} </color>");
    }

    public void DeactivateHitbox()
    {
        _myHitbox.enabled = false;
        Debug.Log($"<color=green> Hitbox State {_myHitbox.enabled} </color>");

    }

    //Kill the motherflipers in the honking way
    void GetEmPrincess(IRagdoll ragdoll, Transform trans)
    {
        Debug.Log($"<color=red> Chu Chu Madafaka (sorry {trans.name}) </color>");

        var dir = (trans.position - transform.position).normalized;

        ragdoll.CallRagdollOn(dir);
        ragdoll.CallRagdollOff(1.5f, true);

    }
}
