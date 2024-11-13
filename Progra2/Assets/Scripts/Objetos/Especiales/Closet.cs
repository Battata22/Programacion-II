using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : SpecialObject
{
    protected override void Awake()
    {
        CreateTrap();
    }

    protected override void ObjectAbility(Transform origin)
    {
        Collider[] coliders = Physics.OverlapSphere(origin.position, _detectRadius, _detectableLayers);
        Debug.Log(coliders.Length);
        foreach (Collider colider in coliders)
        {
            if(colider.transform.TryGetComponent<Asustable>(out Asustable asus))
            {
                if (asus.TryGetComponent<EnableRagdoll>(out EnableRagdoll enRag))
                {
                    var dir = new Vector3(asus.transform.position.x - transform.position.x, asus.transform.position.y + 1 - transform.position.x, asus.transform.position.z - transform.position.z).normalized;
                    enRag.ActivateRagdoll(dir);
                }
                else
                    asus.CallRagdollOn();

                asus.CallRagdollOff(2f, true);

            }
        }
    }
}
