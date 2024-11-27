using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    PlayerTrap parentScript;

    private void Awake()
    {
        parentScript = GetComponentInParent<PlayerTrap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var asus = other.gameObject.GetComponent<Asustable>();
        if (asus && (asus.scared || asus._doubt))
        {
            parentScript.AsustableDetected(asus);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var asus = other.gameObject.GetComponent<Asustable>();
        if (asus && (asus.scared || asus._doubt))
        {
            parentScript.AsustableDetected(asus);
        }
    }
}
