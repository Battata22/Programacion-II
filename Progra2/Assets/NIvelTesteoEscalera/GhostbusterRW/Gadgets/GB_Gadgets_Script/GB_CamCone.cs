using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_CamCone : MonoBehaviour
{
    [SerializeField] GB_Cam gbCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Player>(out var player))
            gbCam.DetectGhost();
    }
}
