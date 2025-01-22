using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatArea : MonoBehaviour
{
    [SerializeField] Cat _cat;
    [SerializeField] LayerMask obstructions;
    [SerializeField] Transform _myHolder;

    bool _inRange;

    bool _active;
    public bool active
    {
        get { return _active; }
        set
        {
            SetActive(value);
        }
    }

    //private void Start()
    //{
    //}

    public void Initialize(Cat newCat,Transform newHolder)
    {
        Debug.Log($"Gato Area Iniciada {newCat.name} {newHolder.name}");

        _cat = newCat;
        _myHolder = newHolder;

        active = true;
    }

    void Update()
    {
        if (!_active)
            return;

        Movement();

        Dev_DrawRay();
    }

    void Movement()
    {
        if (!_active) return;

        transform.position = _myHolder.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_active) return;
        if(other.gameObject.TryGetComponent<Player>(out var player))
        {
            if (CheckLOS(player.transform, transform))
            {
                //_cat.JumpToPLayer();

                _cat.StartAlert();
                _inRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player) && _inRange)
        {
            _inRange = false;
            _cat.StopAlert();
        }
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

    void SetActive(bool state)
    {
        _active = state;
        transform.GetComponent<Collider>().enabled = state;
    }
    // Developer Test

    Transform dev_origin;
    Vector3 dev_dir;
    void Dev_DrawRay()
    {
        if (dev_origin == null || dev_dir == Vector3.zero) return;

        Debug.DrawRay(dev_origin.position, dev_dir);
    }
}
