using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaBehaviour : MonoBehaviour
{
    [SerializeField] float checkCooldown, radioCheck;
    [SerializeField] LayerMask targetLayer;
    float waitCheck;
    int listaNumber;
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
        listaNumber = 0;
        Collider[] collCercanos = Physics.OverlapSphere(transform.position, radioCheck, targetLayer);
        if (collCercanos.Length >= 1)
        {
            //int amountSelected = Random.Range(0, collCercanos.Length % 3);
            //if (amountSelected == 0)
            //{
            //    amountSelected = 1;
            //    print("se puso en 1");
            //}

            int amountSelected = Random.Range(1, 2 + 1);

            //if (listaNumber < amountSelected)
            //{
            //    int tocatoca = Random.Range(1, collCercanos.Length);
            //    if (collCercanos[tocatoca].gameObject.GetComponent<Pickable>().blessed == false)
            //    {
            //        collCercanos[tocatoca].gameObject.GetComponent<Pickable>().blessed = true;
            //        print(collCercanos[tocatoca].gameObject.name);
            //        print("toca = " + tocatoca + " amoun = " + amountSelected + " col lenght " + collCercanos.Length);
            //    }
            //    listaNumber++;

            //}

            for (int i = 0; i < amountSelected; i++)
            {
                int tocatoca = Random.Range(1, collCercanos.Length);
                if (collCercanos[tocatoca].gameObject.GetComponent<Pickable>().blessed == false)
                {
                    collCercanos[tocatoca].gameObject.GetComponent<Pickable>().blessed = true;
                    print(collCercanos[tocatoca].gameObject.name);
                    print("toca = " + tocatoca + " amoun = " + amountSelected + " col lenght " + collCercanos.Length);
                }
                listaNumber++;
            }
        }

        waitCheck = 0;
    }
}
