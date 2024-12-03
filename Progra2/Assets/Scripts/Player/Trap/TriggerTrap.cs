using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    PlayerTrap parentScript;
    bool isShadow = false;

    private void Awake()
    {
        parentScript = GetComponentInParent<PlayerTrap>();

        if(SelectorUI.habAct == 2)
            isShadow = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var asus = other.gameObject.GetComponent<Asustable>();
        if (asus && (asus.scared || asus._doubt))
        {
            parentScript.AsustableDetected(asus);
        }

        if (isShadow && other.TryGetComponent<Ghostbuster>(out var gb))
        {
            parentScript.Interact(gb);
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
