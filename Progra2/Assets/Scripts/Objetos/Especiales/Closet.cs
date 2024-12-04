using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Closet : SpecialObject, ILockeable
{
    [SerializeField] GameObject jumpScaredPrefab;
    [SerializeField] Asustable _target;
    [SerializeField] bool locked;

    bool inPos = false, trapActive = false;


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
        if(trapActive && !inPos && Vector3.SqrMagnitude(_target.transform.position - transform.position) < (_detectRadius * 0.8) * (_detectRadius * 0.8))
        {
            inPos = true;
        }
    }

    protected override void ObjectAbility(Transform origin)
    {
        _target.GetDoubt(transform.position);

        trapActive = true;

        StartCoroutine(WaitToScare(origin));
    }

    IEnumerator WaitToScare(Transform origin)
    {
        while (inPos == false)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        JumpScare(origin);
        GameManager.Instance.pasoActual = 1;
    }

    void JumpScare(Transform origin)
    {
        Collider[] coliders = Physics.OverlapSphere(origin.position, _detectRadius, _detectableLayers);
        Debug.Log(coliders.Length);

        if (coliders.Length <= 0)
        {
            Debug.Log($"<color=red> NINGUN ASUSTABLE EN RANGO DE {transform.name} </color>");
            return;
        }

        Instantiate(jumpScaredPrefab, transform.position, Quaternion.identity);

        ActionActive();
        foreach (Collider colider in coliders)
        {
            if (colider.transform.TryGetComponent<Asustable>(out Asustable asus))
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

        trapActive = false;
        Destroy(_trap);
    }
}
