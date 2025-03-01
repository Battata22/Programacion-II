using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GB_Boss.isInSafeZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GB_Boss.isInSafeZone = false;
    }

}
