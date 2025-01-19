using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class ChildGusDetector : MonoBehaviour
{
    [SerializeField] ChildScript _myOwner;
    [SerializeField] LayerMask obstructions;

    bool _active = false;
    //private void Start()
    //{
    //    _myOwner = GetComponentInParent<ChildScript>();
    //}

    public void Initialize(ChildScript newOwner)
    {
        _myOwner = newOwner;
        _active = true;
        transform.GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        if (!_active) return;
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (_myOwner != null && CheckLOS(other.transform, transform))
            {
                Debug.Log("<color=green>Gus detectado</color>");
                
                _myOwner.StartChase();
            }
        }
    }

    void Movement()
    {
        transform.position = _myOwner.detectorOrigin.position;
    }

    bool CheckLOS(Transform player, Transform owner)
    {
        var dir = player.position - owner.position;
        //var dist = dir.magnitude;

        dev_dir = dir;
        dev_origin = owner;

        RaycastHit hit;
        if (Physics.Raycast(owner.position, dir, out hit, dir.magnitude, obstructions))
        {
            // Devuelve false cuando el rayo es cortado por paredes
            //Debug.Log($"<color=green> Rayo cortado por {hit.transform.name} </color>");
            return false;
        }
        else
        {
            //Debug.Log($"<color=red> No se corto el rayo </color>");
            return true;
        }
    }

    Transform dev_origin;
    Vector3 dev_dir;
    void Dev_DrawRay()
    {
        if (dev_origin == null || dev_dir == Vector3.zero) return;

        Debug.DrawRay(dev_origin.position, dev_dir);
    }
}
