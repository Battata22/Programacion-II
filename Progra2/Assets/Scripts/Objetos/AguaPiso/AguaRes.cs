 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AguaRes : MonoBehaviour
{
    Collider col;
    [SerializeField] float slideForce, fuerzaSaltoCat;


    void Start()
    {
        col = GetComponent<Collider>();
    }


    void Update()
    {
        
    }

    #region Comment
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<NPC>(out NPC npc))
    //    {
    //        other.TryGetComponent<Rigidbody>(out Rigidbody _rbNPC);
    //        _rbNPC.drag = 0;
    //        _rbNPC.angularDrag = 0;
    //        _rbNPC.velocity += new Vector3(1,1,1) * slideForce;
    //    }
    //} 
    #endregion
    
    private void OnTriggerStay(Collider other)
    {
        //if (other.TryGetComponent<Cat>(out Cat catScript))
        //{
        //    other.TryGetComponent<Rigidbody>(out Rigidbody _rbcat);
        //    other.TryGetComponent<NavMeshAgent>(out NavMeshAgent nav);
        //    nav.enabled = false;
        //    _rbcat.AddForce(other.transform.up * fuerzaSaltoCat);
        //}

        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            other.TryGetComponent<Rigidbody>(out Rigidbody _rbNPC);
            _rbNPC.drag = 0;
            _rbNPC.angularDrag = 0;
            _rbNPC.AddForce(other.transform.forward * slideForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            other.TryGetComponent<Rigidbody>(out Rigidbody _rbNPC);
            _rbNPC.drag = 3;
            _rbNPC.angularDrag = 0.05f;
        }

        //if (other.TryGetComponent<Cat>(out Cat catScript))
        //{
        //    other.TryGetComponent<NavMeshAgent>(out NavMeshAgent nav);
        //    nav.enabled = true;
        //}
    }
}
