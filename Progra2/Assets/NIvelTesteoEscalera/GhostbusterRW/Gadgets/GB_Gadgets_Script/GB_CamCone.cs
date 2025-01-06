using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_CamCone : MonoBehaviour
{
    [SerializeField] GB_Cam gbCam;
    [SerializeField] LayerMask obstructions;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Player>(out var player))
        { 
            //gbCam.DetectGhost();
            if (CheckLOS(player.transform, transform))
            {
                // Player detectado, llamar GB
                Debug.Log($"<color=green> Gus Detectado </color>");
                gbCam.DetectGhost();
            }
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
            Debug.Log($"<color=green> Rayo cortado por {hit.transform.name} </color>");
            return false;
        }
        else
        {
            Debug.Log($"<color=red> No se corto el rayo </color>");
            return true;
        }
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
