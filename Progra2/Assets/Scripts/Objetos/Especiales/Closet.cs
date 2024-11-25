using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : SpecialObject, ILockeable
{
    [SerializeField] GameObject jumpScaredPrefab;
    [SerializeField] bool locked;

    public event DelegateType.VoidDelegate ActionActive = delegate { };

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
        CreateTrap();
    }

    protected override void Awake()
    {
        //CreateTrap();
    }

    //Solo para Testeo
    private void Update()
    {
        //if(locked) return;
        //CreateTrap();
    }

    protected override void ObjectAbility(Transform origin)
    {


        Collider[] coliders = Physics.OverlapSphere(origin.position, _detectRadius, _detectableLayers);
        Debug.Log(coliders.Length);

        if(coliders.Length <= 0 ) 
        {
            Debug.Log($"<color=red> NINGUN ASUSTABLE EN RANGO DE {transform.name} </color>");
            return;
        }

        Instantiate(jumpScaredPrefab, transform.position, Quaternion.identity);

        ActionActive();
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
