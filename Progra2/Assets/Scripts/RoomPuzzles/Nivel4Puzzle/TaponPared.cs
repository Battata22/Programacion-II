using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaponPared : MonoBehaviour
{
    //a
    [SerializeField] GameObject _poster;
    [SerializeField] Mesh _posterRoto;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Pickable>())
        {
            _poster.GetComponent<MeshFilter>().mesh = _posterRoto;
        }
    }

    public void ChangeToTrigger()
    {
        var shit = GetComponent<Collider>();
        shit.isTrigger = true;
    }
}
