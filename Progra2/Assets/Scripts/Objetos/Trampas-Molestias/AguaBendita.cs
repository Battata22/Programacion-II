using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaBendita : MonoBehaviour
{
    [SerializeField] float radioCheck;
    [SerializeField] LayerMask targetLayer;

    void Start()
    {
        gameObject.GetComponent<Pickable>().aguaRompible = true;
    }


    void Update()
    {
        
    }

    private void OnDestroy()
    {
        BlessEverything();
    }

    public void BlessEverything()
    {
        Collider[] collCercanos = Physics.OverlapSphere(transform.position, radioCheck, targetLayer);
        foreach (Collider coll in collCercanos)
        {
            if(coll.gameObject.TryGetComponent<IBlessable>(out var coso))
                coso.GetBlssed();
        }
        
    }
}
