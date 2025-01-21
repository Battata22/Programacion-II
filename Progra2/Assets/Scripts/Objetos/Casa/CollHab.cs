using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollHab : MonoBehaviour
{
    bool item = false, npc = false, asustado = false;
    Asustable asScript;
    Pickable pickableScript;

    void Update()
    {
        if (item && npc && !asustado)
        {
            asScript.GetScared(pickableScript._scareAmount,-1);// ahora scare usa index room porque soy un enfermo
            asustado = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Pickable>(out Pickable pickScript))
        {
            item = true;
            pickableScript = pickScript;
        }

        if (other.TryGetComponent<Asustable>(out Asustable asusScript))
        {
            npc = true;
            asScript = asusScript;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Pickable>(out Pickable pickScript))
        {
            item = false;
            asustado = false;
        }

        if (other.TryGetComponent<Asustable>(out Asustable asusScript))
        {
            npc = false;
            asustado = false;
        }
    }
}
