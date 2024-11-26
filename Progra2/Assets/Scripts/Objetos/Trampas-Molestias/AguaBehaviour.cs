using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaBehaviour : MonoBehaviour
{
    [SerializeField] float checkCooldown, radioCheck;
    [SerializeField] LayerMask targetLayer;
    float waitCheck;
    int listaNumber;
    

    public void Bless()
    {
        listaNumber = 0;
        Collider[] collCercanos = Physics.OverlapSphere(transform.position, radioCheck, targetLayer);
        if (collCercanos.Length >= 1)
        {
            #region Otra cosa pero antes
            //int amountSelected = Random.Range(0, collCercanos.Length % 3);
            //if (amountSelected == 0)
            //{
            //    amountSelected = 1;
            //    print("se puso en 1");
            //} 
            #endregion

            int amountSelected = Random.Range(1, 2 + 1);

            #region Cosa
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
            #endregion

            for (int i = 0; i < amountSelected; i++)
            {
                int tocatoca = Random.Range(0, collCercanos.Length);
                //Debug.Log($"<color=blue> Tamanho de lista {collCercanos.Length - 1} indice elegido {tocatoca}</color>");
                if (collCercanos[tocatoca].gameObject.GetComponent<Pickable>().blessed == false)
                {
                    collCercanos[tocatoca].gameObject.GetComponent<IBlessable>().GetBlssed();
                    //print(collCercanos[tocatoca].gameObject.name);
                    //print("toca = " + tocatoca + " amoun = " + amountSelected + " col lenght " + collCercanos.Length);
                }
                listaNumber++;
            }
        }

        waitCheck = 0;
    }
}
