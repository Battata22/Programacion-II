using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaBehaviour : MonoBehaviour
{
    [SerializeField] float checkCooldown, radioCheck;
    //[SerializeField] int cantMaximaBendita;
    [SerializeField] bool chequeado = false;
    [SerializeField] LayerMask targetLayer;
    float waitCheck;
    void Start()
    {

    }


    void Update()
    {
        waitCheck += Time.deltaTime;

        if (waitCheck >= checkCooldown)
        {
            Checking();
        }
    }

    void Checking()
    {
        Collider[] collCercanos = Physics.OverlapSphere(transform.position, radioCheck, targetLayer);
        if (collCercanos.Length > 1)
        {
            int amountSelected = Random.Range(0, 1 + 1);
            collCercanos[Random.Range(0, collCercanos.Length + 1)].gameObject.TryGetComponent<Pickable>(out Pickable pickScript);
        }

        waitCheck = 0;
    }
}
