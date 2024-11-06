using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charco : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Asustable>(out Asustable asusScript))
        {
            asusScript.StartSlow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Asustable>(out Asustable asusScript))
        {
            asusScript.StopSlow();
        }
    }
}
